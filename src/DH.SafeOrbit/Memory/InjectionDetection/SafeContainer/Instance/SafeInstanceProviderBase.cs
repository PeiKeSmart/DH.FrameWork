﻿using System;
using DG.SafeOrbit.Exceptions;
using DG.SafeOrbit.Memory.Injection;
using DG.SafeOrbit.Memory.InjectionServices;
using DG.SafeOrbit.Memory.InjectionServices.Protectable;

namespace DG.SafeOrbit.Memory.SafeContainerServices.Instance.Providers
{
    /// <summary>
    ///     Provides helper methods and base protection for <see cref="IInstanceProvider" />'s.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <seealso cref="InstanceProtectionMode" />
    /// <seealso cref="IInstanceProvider" />
    /// <seealso cref="IInjectionDetector" />
    /// <seealso cref="IProtectable{TProtectionLevel}" />
    /// <seealso cref="ProtectableBase{InstanceProtectionMode}" />
    internal abstract class SafeInstanceProviderBase<TImplementation> :
        ProtectableBase<InstanceProtectionMode>, IInstanceProvider
    {
        private readonly IInjectionDetector _injectionDetector;
        private bool _isFirstTime = true;

        protected SafeInstanceProviderBase(LifeTime lifeTime, InstanceProtectionMode initialProtectionMode)
            : this(lifeTime, new InjectionDetector(), initialProtectionMode, Defaults.AlertChannel)
        {
        }

        protected SafeInstanceProviderBase(LifeTime lifeTime, IInjectionDetector injectionDetector,
            InstanceProtectionMode protectionMode, InjectionAlertChannel alertChannel)
            : base(protectionMode)
        {
            _injectionDetector = injectionDetector ?? throw new ArgumentNullException(nameof(injectionDetector));
            LifeTime = lifeTime;
            AssemblyQualifiedName = typeof(TImplementation).AssemblyQualifiedName;
            AlertChannel = alertChannel;
            ChangeProtectionMode(new ProtectionChangeContext<InstanceProtectionMode>(protectionMode));
        }


        public string AssemblyQualifiedName { get; }

        /// <summary>
        ///     Determines whether this instance provider's instance is allowed be protected against state injections. Default:
        ///     <c>TRUE</c>
        /// </summary>
        /// <seealso cref="IInjectionDetector.ScanState" />
        /// <seealso cref="Provide" />
        public virtual bool CanProtectState { get; } = true;

        public Type ImplementationType => typeof(TImplementation);
        public LifeTime LifeTime { get; set; }

        public object Provide()
        {
            var instance = GetInstance();
            if (_isFirstTime)
            {
                //Verify lazily when the first instance is requested.
                VerifyInstanceInternal(instance);
                _isFirstTime = false;
            }
            else
            {
                if (CanAlert && _injectionDetector.CanAlert)
                    _injectionDetector.AlertUnnotifiedChanges(instance);
            }

            return instance;
        }

        public InjectionAlertChannel AlertChannel
        {
            get => _injectionDetector.AlertChannel;
            set => _injectionDetector.AlertChannel = value;
        }

        public bool CanAlert => CurrentProtectionMode != InstanceProtectionMode.NoProtection;
        public abstract TImplementation GetInstance();

        protected sealed override void ChangeProtectionMode(IProtectionChangeContext<InstanceProtectionMode> context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            switch (context.NewValue)
            {
                case InstanceProtectionMode.StateAndCode:
                    _injectionDetector.ScanCode = true;
                    _injectionDetector.ScanState = CanProtectState;
                    if (!_isFirstTime && context.OldValue != InstanceProtectionMode.JustState)
                        VerifyInstanceInternal(GetInstance());
                    break;
                case InstanceProtectionMode.JustCode:
                    _injectionDetector.ScanCode = true;
                    _injectionDetector.ScanState = false;
                    break;
                case InstanceProtectionMode.JustState:
                    _injectionDetector.ScanCode = false;
                    _injectionDetector.ScanState = CanProtectState;
                    if (!_isFirstTime && context.OldValue != InstanceProtectionMode.StateAndCode)
                        VerifyInstanceInternal(GetInstance());
                    break;
                case InstanceProtectionMode.NoProtection:
                    _injectionDetector.ScanCode = false;
                    _injectionDetector.ScanState = false;
                    break;
                default:
                    throw new UnexpectedEnumValueException<InstanceProtectionMode>(context.NewValue);
            }
        }

        private void VerifyInstanceInternal(TImplementation instance)
        {
            var canProtectState = _injectionDetector.ScanState = _injectionDetector.ScanState && CanProtectState;
            if (canProtectState || _isFirstTime) _injectionDetector.NotifyChanges(instance);
        }
    }
}
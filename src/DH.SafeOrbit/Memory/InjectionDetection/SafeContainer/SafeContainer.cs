﻿using System;
using System.Collections.Generic;
using System.Linq;
using DG.SafeOrbit.Exceptions;
using DG.SafeOrbit.Extensions;
using DG.SafeOrbit.Memory.Injection;
using DG.SafeOrbit.Memory.InjectionServices;
using DG.SafeOrbit.Memory.InjectionServices.Protectable;
using DG.SafeOrbit.Memory.InjectionServices.Reflection;
using DG.SafeOrbit.Memory.SafeContainerServices.Instance;
using DG.SafeOrbit.Memory.SafeContainerServices.Instance.Providers;
using DG.SafeOrbit.Memory.SafeContainerServices.Instance.Validation;

namespace DG.SafeOrbit.Memory
{
    /// <summary>
    ///     <see cref="SafeContainer" /> is a lightweight object container with memory protection.
    /// </summary>
    /// <seealso cref="ISafeContainer" />
    /// <seealso cref="IServiceProvider" />
    /// <seealso cref="IProtectable{TProtectionLevel}" />
    /// <seealso cref="ProtectableBase{TProtectionLevel}" />
    /// <seealso cref="SafeInstanceProviderBase{TImplementation}" />
    /// <seealso cref="InjectionDetector" />
    public class SafeContainer :
        ProtectableBase<SafeContainerProtectionMode>,
        ISafeContainer
    {
        private readonly IInstanceProviderFactory _instanceFactory;
        private readonly IInstanceValidator _instanceValidator;
        private readonly ISafeObjectFactory _safeObjectFactory;
        private readonly ITypeIdGenerator _typeIdGenerator;
        private InjectionAlertChannel _alertChannel;
        private bool _isVerified;
        private ISafeObject<Dictionary<string, IInstanceProvider>> _typeInstancesSafe;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SafeContainer" /> class.
        /// </summary>
        public SafeContainer() : this(Defaults.ContainerProtectionMode)
        {
        }

        /// <summary>
        ///     Gets a new instance for <see cref="SafeContainer" />.
        /// </summary>
        /// <param name="protectionMode">The protection level of the factory.</param>
        public SafeContainer(SafeContainerProtectionMode protectionMode)
            : this(
                TypeIdGenerator.StaticInstance,
                InstanceProviderFactory.StaticInstance,
                new InstanceValidator(),
                SafeObjectFactory.StaticInstance, protectionMode)
        {
        }

        internal SafeContainer(
            ITypeIdGenerator typeIdGenerator,
            IInstanceProviderFactory instanceFactory,
            IInstanceValidator instanceValidator,
            ISafeObjectFactory safeObjectFactory,
            SafeContainerProtectionMode protectionMode = Defaults.ContainerProtectionMode,
            InjectionAlertChannel alertChannel = Defaults.AlertChannel) : base(protectionMode)
        {
            _typeIdGenerator = typeIdGenerator ?? throw new ArgumentNullException(nameof(typeIdGenerator));
            _instanceFactory = instanceFactory ?? throw new ArgumentNullException(nameof(instanceFactory));
            _instanceValidator = instanceValidator ?? throw new ArgumentNullException(nameof(instanceValidator));
            _safeObjectFactory = safeObjectFactory ?? throw new ArgumentNullException(nameof(safeObjectFactory));
            _alertChannel = alertChannel;
            ChangeProtectionMode(new ProtectionChangeContext<SafeContainerProtectionMode>(protectionMode));
            UpdateAlertChannel(alertChannel);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException"><see cref="Verify" /> is not called.</exception>
        /// <exception cref="MemoryInjectionException">If the object has been changed after last stamp.</exception>
        /// <exception cref="MemoryInjectionException">If the object has been changed after last stamp.</exception>
        /// <exception cref="KeyNotFoundException">If the <typeparamref name="TComponent" /> is not registered.</exception>
        public TComponent Get<TComponent>()
        {
            return (TComponent) GetService(typeof(TComponent));
        }

        /// <inheritdoc />
        public void Register<TComponent>(LifeTime lifeTime = LifeTime.Transient)
            where TComponent : class, new()
        {
            Register<TComponent, TComponent>(lifeTime);
        }

        /// <inheritdoc />
        public void Register<TComponent, TImplementation>(LifeTime lifeTime = LifeTime.Transient)
            where TComponent : class
            where TImplementation : TComponent, new()
        {
            var protectionMode = GetInnerInstanceProtectionMode(CurrentProtectionMode);
            var instance = _instanceFactory.Get<TImplementation>(lifeTime, protectionMode, AlertChannel);
            RegisterInstanceInternal<TComponent>(instance);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="instanceInitializer" /> is <see langword="null" />.</exception>
        public void Register<TComponent, TImplementation>(Func<TImplementation> instanceInitializer,
            LifeTime lifeTime = LifeTime.Unknown) where TComponent : class where TImplementation : TComponent, new()
        {
            if (instanceInitializer == null) throw new ArgumentNullException(nameof(instanceInitializer));
            var protectionMode = GetInnerInstanceProtectionMode(CurrentProtectionMode);
            var instance = _instanceFactory.Get(instanceInitializer, protectionMode, AlertChannel, lifeTime);
            RegisterInstanceInternal<TComponent>(instance);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="instanceInitializer" /> is <see langword="null" />.</exception>
        public void Register<TComponent>(Func<TComponent> instanceInitializer, LifeTime lifeTime = LifeTime.Unknown)
            where TComponent : class
        {
            var protectionMode = GetInnerInstanceProtectionMode(CurrentProtectionMode);
            if (instanceInitializer == null) throw new ArgumentNullException(nameof(instanceInitializer));
            var instance = _instanceFactory.Get(instanceInitializer, protectionMode, AlertChannel, lifeTime);
            RegisterInstanceInternal<TComponent>(instance);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">If no type is registered in the current <see cref="SafeContainer" /> instance.</exception>
        /// <exception cref="ArgumentException">If the instance of <see cref="SafeContainer" /> is already verified.</exception>
        public void Verify()
        {
            if (!_typeInstancesSafe.Object.Any())
                throw new ArgumentException(
                    $"No type is registered in the {nameof(SafeContainer)} before calling {nameof(Verify)}. Please register types using {nameof(Register)} method.");
            if (_isVerified)
                throw new ArgumentException($"This instance of {nameof(SafeContainer)} is already verified.");
#if DEBUG
            var instanceProviders = _typeInstancesSafe.Object.Values.ToList().AsReadOnly();
            _instanceValidator.ValidateAll(instanceProviders);
#endif
            _isVerified = true;
            // _typeInstancesSafe.MakeReadOnly(); even though it's desired, it blocks settings AlertChannel later on.
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException"><see cref="SafeContainer.Verify" /> is not called.</exception>
        /// <exception cref="MemoryInjectionException">If the object has been changed after last stamp.</exception>
        /// <exception cref="MemoryInjectionException">If the object has been changed after last stamp.</exception>
        /// <exception cref="KeyNotFoundException">
        ///     If the <paramref name="serviceType" /> is not
        ///     registered.
        /// </exception>
        public object GetService(Type serviceType)
        {
            if (!_isVerified) throw new ArgumentException($"Please call {nameof(Verify)} before using the factory");
            SpinUntilSecurityModeIsAvailable();
            var key = _typeIdGenerator.Generate(serviceType);
            if (!_typeInstancesSafe.Object.TryGetValue(key, out var instanceProvider))
                throw new KeyNotFoundException($"{serviceType.FullName} is not registered.");
            var result = instanceProvider.Provide();
            return result;
        }

        /// <summary>
        ///     Gets or sets the alert channel for the inner <see cref="IInjectionDetector" />.
        /// </summary>
        /// <seealso cref="IAlerts" />
        /// <seealso cref="IInjectionDetector" />
        /// <seealso cref="CanAlert" />
        /// <value>The alert channel.</value>
        public virtual InjectionAlertChannel AlertChannel
        {
            get => _alertChannel;
            set
            {
                if (_alertChannel == value) return;
                UpdateAlertChannel(value);
            }
        }

        /// <inheritdoc />
        public bool CanAlert => CurrentProtectionMode != SafeContainerProtectionMode.NonProtection;

        private void RegisterInstanceInternal<TComponent>(IInstanceProvider instance) where TComponent : class
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var key = _typeIdGenerator.Generate<TComponent>();
            SpinUntilSecurityModeIsAvailable();
            if (_typeInstancesSafe.Object.ContainsKey(key))
                throw new ArgumentException($"{typeof(TComponent).FullName} is already registered.");
            _typeInstancesSafe.ApplyChanges(dic => { dic.Add(key, instance); });
        }

        private ISafeObject<Dictionary<string, IInstanceProvider>> CreateInnerDictionary(
            SafeContainerProtectionMode protectionMode, InjectionAlertChannel alertChannel,
            Dictionary<string, IInstanceProvider> initialObject = null)
        {
            var innerDictionaryProtection = GetInnerDictionaryObjectProtectionMode(protectionMode);
            var settings = new InitialSafeObjectSettings
            (
                protectionMode: innerDictionaryProtection,
                isReadOnly: false,
                initialValue: initialObject,
                alertChannel: alertChannel
            );
            var result = _safeObjectFactory.Get<Dictionary<string, IInstanceProvider>>(settings);
            return result;
        }

        private void UpdateAlertChannel(InjectionAlertChannel value)
        {
            if (!Enum.IsDefined(typeof(InjectionAlertChannel), value))
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Value should be defined in the InjectionAlertChannel enum.");
            _alertChannel = value;
            _typeInstancesSafe.AlertChannel = value;
            var dict = _typeInstancesSafe.Object;
            if (dict?.Values.Any(v => !v.AlertChannel.Equals(value)) == true)
                _typeInstancesSafe.ApplyChanges(dic => dic?
                    .Values
                    .Where(c => !c.AlertChannel.Equals(value))
                    .ForEach(instance => instance.AlertChannel = value));
        }


        /// <summary>
        ///     Changes the protection mode of the inner dictionary and its values.
        /// </summary>
        protected sealed override void ChangeProtectionMode(
            IProtectionChangeContext<SafeContainerProtectionMode> context)
        {
            Dictionary<string, IInstanceProvider> newDictionary = null;
            if (_typeInstancesSafe != null)
            {
                var innerInstanceProtection = GetInnerInstanceProtectionMode(context.NewValue);
                newDictionary = _typeInstancesSafe.Object;
                newDictionary.Values.ForEach(instance => instance.SetProtectionMode(innerInstanceProtection));
                _typeInstancesSafe.Dispose();
            }
            // Re-create inner safe object
            _typeInstancesSafe = CreateInnerDictionary(context.NewValue, AlertChannel, newDictionary);
        }

        private static InstanceProtectionMode GetInnerInstanceProtectionMode(SafeContainerProtectionMode protectionMode)
        {
            return protectionMode switch
            {
                SafeContainerProtectionMode.FullProtection => InstanceProtectionMode.StateAndCode,
                SafeContainerProtectionMode.NonProtection => InstanceProtectionMode.NoProtection,
                _ => throw new ArgumentOutOfRangeException(nameof(protectionMode), protectionMode, null)
            };
        }

        private static SafeObjectProtectionMode GetInnerDictionaryObjectProtectionMode(
            SafeContainerProtectionMode protectionMode)
        {
            return protectionMode switch
            {
                SafeContainerProtectionMode.FullProtection => SafeObjectProtectionMode.JustState, // No need to protect type of the dictionary
                SafeContainerProtectionMode.NonProtection => SafeObjectProtectionMode.NoProtection,
                _ => throw new ArgumentOutOfRangeException(nameof(protectionMode), protectionMode, null)
            };
        }
    }
}
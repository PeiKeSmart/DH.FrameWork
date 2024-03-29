﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.SafeOrbit.Exceptions;
using DG.SafeOrbit.Memory.Injection;
using DG.SafeOrbit.Memory.InjectionServices;
using DG.SafeOrbit.Memory.InjectionServices.Protectable;

namespace DG.SafeOrbit.Memory
{
    /// <summary>
    ///     <p>
    ///         <see cref="SafeObject{TObject}" /> uses <seealso cref="InjectionDetector" /> and tracks any outside
    ///         modifications of the <typeparamref name="TObject" />.
    ///     </p>
    ///     <p>
    ///         Provides thread safe access to its inner object.
    ///     </p>
    ///     <p>
    ///         <b>Important:</b> Use only <see cref="ApplyChanges" /> to change the state of your object. It'll authorize your
    ///         access and apply them securely.
    ///     </p>
    ///     <p>
    ///         Its security is based on <see cref="IInjectionDetector" /> and alerts if unknown changes to the object occurs
    ///         based on current <see cref="SafeObjectProtectionMode" />.
    ///     </p>
    /// </summary>
    /// <example>
    ///     <p>
    ///         Usage:
    ///         <code>
    ///           var safeObject = new SafeObject&lt;Customer&gt;(/*leaving default instance empty in constructor to get a new instance*/);
    ///           //you can alternatively use an existing instance to protect: new SafeObject&lt;TObject&gt;(new InitialSafeObjectSettings(initialValue, true));
    ///           //each change to the object's state or code must be using ApplyChanges
    ///           safeObject.ApplyChanges((customer) => customer.Id = 5);
    ///           //retrieve safe data
    ///           var customerId = safeObject.Object.Id; //returns 5 or alerts if any injection is detected
    ///           }
    ///     </code>
    ///     </p>
    ///     <p>
    ///         Advanced settings :
    ///         <code>
    ///           //if the object's Id property becomes 0 by any non-applied change,
    ///           //the SafeObject instance will alert a memory injection depending on its protection mode.
    ///           //safeObject.SetProtectionMode(SafeObjectProtectionMode.StateAndCode); //changes its protection mode to no protection
    ///           //you can change the alert channel:
    ///           safeObject.AlertChannel = InjectionAlertChannel.DebugWrite; //any detected injections will be alerted using the alert channel
    ///           safeObject.SetProtectionMode(SafeObjectProtectionMode.NoProtection); //stops the protection of object,
    ///           //SafeObject will never alert when it's not protected.
    ///           var willAlert = safeObject.CanAlert; //returns false as the instance will only alert when it's protected.
    ///     </code>
    ///     </p>
    /// </example>
    /// <seealso cref="SafeObjectProtectionMode" />
    /// <seealso cref="IAlerts" />
    /// <seealso cref="ISafeObject{TObject}" />
    /// <seealso cref="IProtectable{TProtectionLevel}" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="IInjectionDetector" />
    /// <seealso cref="InjectionDetector" />
    /// <seealso cref="MemoryInjectionException" />
    /// <seealso cref="ApplyChanges" />
    public class SafeObject<TObject> :
        ProtectableBase<SafeObjectProtectionMode>,
        ISafeObject<TObject>
        where TObject : class, new()
    {
        private readonly IInjectionDetector _injectionDetector;
        private readonly object _syncRoot = new object();
        private readonly TObject _object;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SafeObject{TObject}" /> class using default
        ///     <see cref="IInitialSafeObjectSettings" /> and <see cref="InjectionAlertChannel" />.
        /// </summary>
        /// <seealso cref="IInitialSafeObjectSettings" />
        /// <seealso cref="InjectionAlertChannel" />
        /// <seealso cref="Defaults.SafeObjectSettings" />
        /// <seealso cref="Defaults.AlertChannel" />
        public SafeObject() : this(Defaults.SafeObjectSettings)
        {
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="SafeObject{TObject}" /> class using custom settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException"><paramref name="settings" /> is <see langword="null" /> </exception>
        public SafeObject(IInitialSafeObjectSettings settings) :
            this(settings, new InjectionDetector())
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        ///     <p>Initializes a new instance of the <see cref="SafeObject{TObject}" /> class.</p>
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="injectionDetector">The injection protector.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="settings" /> is not null and <see cref="InitialSafeObjectSettings.InitialValue" /> is not a type
        ///     of <typeparamref name="TObject" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <p><paramref name="injectionDetector" /> is <see langword="null" />.</p>
        ///     <p>
        ///         Initial object value from <paramref name="settings" /> is null or cannot be casted to
        ///         <typeparamref name="TObject" />
        ///     </p>
        /// </exception>
        internal SafeObject(IInitialSafeObjectSettings settings, IInjectionDetector injectionDetector)
            : base(settings.ProtectionMode)
        {
            _injectionDetector = injectionDetector ?? throw new ArgumentNullException(nameof(injectionDetector));
            _injectionDetector.AlertChannel = settings.AlertChannel;
            SetInitialValue(settings.InitialValue, ref _object);
            ChangeProtectionMode(new ProtectionChangeContext<SafeObjectProtectionMode>(settings.ProtectionMode));
            IsReadOnly = false;
            IsReadOnly = settings.IsReadOnly;
        }


        /// <inheritdoc />
        /// <seealso cref="IAlerts" />
        /// <seealso cref="IInjectionDetector" />
        /// <seealso cref="CanAlert" />
        public virtual InjectionAlertChannel AlertChannel
        {
            get => _injectionDetector.AlertChannel;
            set => _injectionDetector.AlertChannel = value;
        }

        /// <inheritdoc />
        /// <seealso cref="IAlerts" />
        /// <seealso cref="IInjectionDetector" />
        public bool CanAlert
            => CurrentProtectionMode != SafeObjectProtectionMode.NoProtection && _injectionDetector.CanAlert;

        /// <inheritdoc />
        /// <exception cref="MemoryInjectionException" accessor="get">If the object has been changed after last stamp.</exception>
        public TObject Object => GetObjectInternal(true);

        /// <inheritdoc />
        public bool IsReadOnly { get; private set; }

        /// <inheritdoc />
        public void MakeReadOnly()
        {
            lock (_syncRoot)
            {
                if (IsReadOnly)
                    return;
                IsReadOnly = true;
            }
        }

        /// <inheritdoc />
        /// <exception cref="WriteAccessDeniedException">
        ///     This instance of <typeparamref name="TObject" /> is marked as
        ///     ReadOnly.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="modification" /> is <see langword="null" /> </exception>
        public void ApplyChanges(Action<TObject> modification)
        {
            if (modification == null) throw new ArgumentNullException(nameof(modification));
            ThrowIfReadOnly();
            VerifyChangesInternal(modification, true);
        }

        /// <summary>
        ///     Verifies the changes to the inner <see cref="InjectionDetector" />.
        /// </summary>
        /// <param name="modification">[Optional] The modification to be run .</param>
        /// <param name="alertUnnotifiedChanges">This method alerts unverified changes.</param>
        private void VerifyChangesInternal(Action<TObject> modification, bool alertUnnotifiedChanges)
        {
            lock (_syncRoot)
            {
                var obj = GetObjectInternal(alertUnnotifiedChanges);
                modification?.Invoke(obj);
                _injectionDetector.NotifyChanges(obj);
            }
        }

        private void SetInitialValue(object initialValue, ref TObject obj)
        {
            if (initialValue == null)
            {
                lock (_syncRoot)
                {
                    obj = new TObject();
                }
            }
            else
            {
                if (!(initialValue is TObject))
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(initialValue)} is not a type of {typeof(TObject).FullName}");
                lock (_syncRoot)
                {
                    obj = (TObject) initialValue;
                    if (_object == null)
                        throw new ArgumentNullException(nameof(initialValue),
                            $"{nameof(initialValue)} is null or cannot cast it to ${typeof(TObject).FullName}");
                }
            }
        }


        /// <summary>
        ///     Gets internal object.
        /// </summary>
        /// <param name="alertUnnotifiedChanges">This method alerts unverified changes.</param>
        /// <exception cref="MemoryInjectionException" accessor="get">If the object has been changed after last stamp.</exception>
        private TObject GetObjectInternal(bool alertUnnotifiedChanges)
        {
            if (alertUnnotifiedChanges && CanAlert)
                _injectionDetector.AlertUnnotifiedChanges(_object);
            return _object;
        }

        /// <exception cref="WriteAccessDeniedException">
        ///     This instance of <typeparamref name="TObject" /> is marked as read only.
        /// </exception>
        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
                throw new WriteAccessDeniedException(
                    $"This instance of {typeof(TObject).Name} is marked as ReadOnly. You cannot apply changes into a ReadOnly instance.");
        }

        private static bool IsDictionary(Type t)
        {
            var isDic = typeof(IDictionary).GetTypeInfo().IsAssignableFrom(t);
            if (isDic) return true;
            var isGenericDic = t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>);
            return isGenericDic;
        }


        /// <exception cref="NotSupportedException">
        ///     <typeparamref name="TObject" />is a dictionary. Code protection for dictionary types are
        ///     not yet supported.
        /// </exception>
        protected sealed override void ChangeProtectionMode(
            IProtectionChangeContext<SafeObjectProtectionMode> context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            switch (context.NewValue)
            {
                case SafeObjectProtectionMode.StateAndCode:
                    if (IsDictionary(typeof(TObject)))
                        throw new NotSupportedException(
                            $"{typeof(TObject).Name} is a dictionary. Code protection for dictionary types are not yet supported.");
                    _injectionDetector.ScanCode = true;
                    _injectionDetector.ScanState = true;
                    if (context.OldValue != SafeObjectProtectionMode.JustState) VerifyChangesInternal(null, false);
                    break;
                case SafeObjectProtectionMode.JustState:
                    _injectionDetector.ScanCode = false;
                    _injectionDetector.ScanState = true;
                    if (context.OldValue != SafeObjectProtectionMode.StateAndCode) VerifyChangesInternal(null, false);
                    break;
                case SafeObjectProtectionMode.JustCode:
                    if (IsDictionary(typeof(TObject)))
                        throw new NotSupportedException(
                            $"{typeof(TObject).Name} is a dictionary. Code protection for dictionaries are not yet supported.");
                    _injectionDetector.ScanCode = true;
                    _injectionDetector.ScanState = false;
                    break;
                case SafeObjectProtectionMode.NoProtection:
                    _injectionDetector.ScanCode = false;
                    _injectionDetector.ScanState = false;
                    break;
                default:
                    throw new UnexpectedEnumValueException<SafeObjectProtectionMode>(context.NewValue);
            }
        }

        #region [IDisposable]
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
                _injectionDetector.Dispose();
            _isDisposed = true;
        }
        ~SafeObject() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
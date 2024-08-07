﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DG.SafeOrbit.Memory.InjectionServices.Protectable
{
    /// <summary>
    ///     A base class that provides helper methods for the implementation of
    ///     <see cref="IProtectable{TProtectionLevel}" />.
    /// </summary>
    /// <typeparam name="TProtectionLevel">The type of the t protection level.</typeparam>
    /// <seealso cref="IProtectable{TProtectionLevel}" />
    public abstract class ProtectableBase<TProtectionLevel> :
        IProtectable<TProtectionLevel> where TProtectionLevel : Enum
    {
        /// <summary>
        ///     A flag that indicates mode is being set in any thread.
        /// </summary>
        private volatile bool _isSettingMode;


        protected ProtectableBase(TProtectionLevel protectionMode)
        {
            CurrentProtectionMode = protectionMode;
        }

        /// <inheritdoc />
        public TProtectionLevel CurrentProtectionMode { get; private set; }

        /// <inheritdoc />
        public void SetProtectionMode(TProtectionLevel mode)
        {
            if (mode.Equals(CurrentProtectionMode)) return;
            InternalSetMode(mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SpinUntilSecurityModeIsAvailable()
        {
            if (_isSettingMode) SpinWait.SpinUntil(() => _isSettingMode, 10000);
        }

        /// <summary>
        ///     Must be overridden with a logic while switching happens.
        /// </summary>
        protected abstract void ChangeProtectionMode(IProtectionChangeContext<TProtectionLevel> context);

        /// <summary>
        ///     Calls the <see cref="ChangeProtectionMode" /> method with right context. If the operation is not canceled then
        ///     sets <see cref="CurrentProtectionMode" />
        /// </summary>
        /// <param name="objectProtectionMode">The object protection mode.</param>
        private void InternalSetMode(TProtectionLevel objectProtectionMode)
        {
            if (_isSettingMode)
                throw new InvalidOperationException("Another thread is currently setting the protection level.");
            _isSettingMode = true;
            var context = GetContext(newValue: objectProtectionMode, oldValue: CurrentProtectionMode);
            ChangeProtectionMode(context);
            if (!context.IsCanceled)
                CurrentProtectionMode = objectProtectionMode;
            _isSettingMode = false;
        }

        private static IProtectionChangeContext<TProtectionLevel> GetContext(TProtectionLevel oldValue,
            TProtectionLevel newValue)
        {
            var result = new ProtectionChangeContext<TProtectionLevel>
            (
                newValue: newValue,
                oldValue: oldValue
            );
            return result;
        }
    }
}
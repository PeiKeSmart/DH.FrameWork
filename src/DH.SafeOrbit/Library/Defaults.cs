﻿using DG.SafeOrbit.Library;
using DG.SafeOrbit.Memory;
using DG.SafeOrbit.Memory.InjectionServices;

namespace DG.SafeOrbit
{
    /// <summary>
    ///     Default settings for SafeOrbit classes.
    /// </summary>
    internal static class Defaults
    {
        //SafeObject
        public const SafeObjectProtectionMode ObjectProtectionMode = SafeObjectProtectionMode.StateAndCode;


        //SafeContainer
        public const SafeContainerProtectionMode ContainerProtectionMode = SafeContainerProtectionMode.FullProtection;

        //InjectionProtector
        public const InjectionAlertChannel AlertChannel = InjectionAlertChannel.ThrowException;

        public static IInitialSafeObjectSettings SafeObjectSettings
            => new InitialSafeObjectSettings(null, false, SafeObjectProtectionMode.StateAndCode,
                SafeOrbitCore.Current.AlertChannel);
    }
}
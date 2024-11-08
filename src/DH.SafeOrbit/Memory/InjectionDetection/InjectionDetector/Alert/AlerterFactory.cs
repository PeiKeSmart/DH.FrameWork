﻿using System;
using DG.SafeOrbit.Library;
using DG.SafeOrbit.Memory.InjectionServices.Alerters;

namespace DG.SafeOrbit.Memory.InjectionServices
{
    internal class AlerterFactory : IAlerterFactory
    {
        public IAlerter Get(InjectionAlertChannel channel)
        {
            switch (channel)
            {
                case InjectionAlertChannel.RaiseEvent:
                    return new RaiseEventAlerter(SafeOrbitCore.Current);
                case InjectionAlertChannel.ThrowException:
                    return new ThrowExceptionAlerter();
                case InjectionAlertChannel.DebugFail:
                    return new DebugFailAlerter();
                case InjectionAlertChannel.DebugWrite:
                    return new DebugWriteAlerter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }
    }
}
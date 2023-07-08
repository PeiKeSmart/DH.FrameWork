using System;
using DG.SafeOrbit.Memory.InjectionServices;
using DG.SafeOrbit.Memory.InjectionServices.Protectable;

namespace DG.SafeOrbit.Memory.SafeContainerServices.Instance
{
    internal interface IInstanceProvider :
        IProtectable<InstanceProtectionMode>,
        IAlerts
    {
        Type ImplementationType { get; }
        LifeTime LifeTime { get; }
        object Provide();
    }
}
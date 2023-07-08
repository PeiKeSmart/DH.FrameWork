using DG.SafeOrbit.Memory.InjectionServices.Alerters;

namespace DG.SafeOrbit.Memory.InjectionServices
{
    internal interface IAlerterFactory
    {
        IAlerter Get(InjectionAlertChannel channel);
    }
}
using DG.SafeOrbit.Memory.Injection;

namespace DG.SafeOrbit.Memory.InjectionServices.Alerters
{
    internal interface IAlerter
    {
        InjectionAlertChannel Channel { get; }
        void Alert(IInjectionMessage info);
    }
}
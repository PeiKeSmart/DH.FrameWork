using System.Diagnostics;
using DG.SafeOrbit.Memory.Injection;

namespace DG.SafeOrbit.Memory.InjectionServices.Alerters
{
    internal class DebugFailAlerter : IAlerter
    {
        public InjectionAlertChannel Channel { get; } = InjectionAlertChannel.DebugFail;

        public void Alert(IInjectionMessage info)
        {
            Debug.Fail(info.ToString());
        }
    }
}
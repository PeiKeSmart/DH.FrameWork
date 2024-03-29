using DG.SafeOrbit.Library;
using DG.SafeOrbit.Memory.InjectionServices.Alerters;

namespace DG.SafeOrbit.Memory.InjectionServices
{
    public enum InjectionAlertChannel
    {
        /// <summary>
        ///     Raises the <see cref="SafeOrbitCore.LibraryInjected" />
        /// </summary>
        /// <seealso cref="RaiseEventAlerter" />
        RaiseEvent,

        /// <summary>
        ///     Throws an exception.
        /// </summary>
        /// <seealso cref="ThrowExceptionAlerter" />
        ThrowException,

        /// <summary>
        ///     Fails on debug mode.
        /// </summary>
        /// <seealso cref="DebugFailAlerter" />
        DebugFail,

        /// <summary>
        ///     Logs on debug mode.
        /// </summary>
        /// <seealso cref="DebugWriteAlerter" />
        DebugWrite
    }
}
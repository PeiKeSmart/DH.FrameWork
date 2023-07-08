using System;
using System.Threading.Tasks;
using DG.SafeOrbit.Library.Build;
using DG.SafeOrbit.Memory;
using DG.SafeOrbit.Memory.Injection;

namespace DG.SafeOrbit.Library
{
    /// <summary>
    ///     Abstracts the class to access inner library behavior.
    /// </summary>
    public interface ISafeOrbitCore
    {
        /// <summary>
        ///     Safe object container that's being used by the library.
        /// </summary>
        ISafeContainer Factory { get; }

        /// <summary>
        ///     Gets the information regarding to current build of SafeOrbit.
        /// </summary>
        IBuildInfo BuildInfo { get; }

        /// <summary>
        ///     Loads the necessary data early on. For better performance, it's highly recommended to start the
        ///     application early in your application start.
        /// </summary>
        Task StartEarlyAsync();

        event EventHandler<IInjectionMessage> LibraryInjected;
    }
}
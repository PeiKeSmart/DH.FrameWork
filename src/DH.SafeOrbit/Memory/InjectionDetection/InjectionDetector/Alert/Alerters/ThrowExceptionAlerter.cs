using DG.SafeOrbit.Exceptions;
using DG.SafeOrbit.Memory.Injection;

namespace DG.SafeOrbit.Memory.InjectionServices.Alerters
{
    /// <summary>
    ///     Throws <see cref="MemoryInjectionException" />.
    /// </summary>
    internal class ThrowExceptionAlerter : IAlerter
    {
        public InjectionAlertChannel Channel { get; } = InjectionAlertChannel.ThrowException;

        /// <exception cref="MemoryInjectionException">
        ///     <p>
        ///         The state of the object has been changed after last validation.
        ///         <seealso cref="InjectionType.VariableInjection" />
        ///     </p>
        ///     <p>
        ///         The code of the object has been changed after last validation.
        ///         <seealso cref="InjectionType.CodeInjection" />
        ///     </p>
        ///     <p>
        ///         Both state and the code code of the object has been changed after last validation.
        ///         <seealso cref="InjectionType.CodeAndVariableInjection" />
        ///     </p>
        /// </exception>
        public void Alert(IInjectionMessage info)
        {
            throw new MemoryInjectionException(info);
        }
    }
}
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DG.SafeOrbit.Exceptions
{
    /// <summary>
    ///     This type of exception is thrown when trying to modify an object with only read only access.
    /// </summary>
    [Serializable]
    public class WriteAccessDeniedException : SafeOrbitException
    {
        public WriteAccessDeniedException(string message) : base(message)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public WriteAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
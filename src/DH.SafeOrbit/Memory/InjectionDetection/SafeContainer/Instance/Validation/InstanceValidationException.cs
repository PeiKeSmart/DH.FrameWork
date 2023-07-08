using DG.SafeOrbit.Exceptions;

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DG.SafeOrbit.Memory.SafeContainerServices.Instance.Validation
{
    [Serializable]
    public class InstanceValidationException : SafeOrbitException
    {
        public InstanceValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public InstanceValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
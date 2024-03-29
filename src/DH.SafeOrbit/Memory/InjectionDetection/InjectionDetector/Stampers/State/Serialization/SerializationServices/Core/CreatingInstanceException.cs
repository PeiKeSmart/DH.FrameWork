﻿//This is a modified version of the beautiful SharpSerializer by Pawel Idzikowski (see: http://www.sharpserializer.com)

using System.Runtime.Serialization;

namespace DG.SafeOrbit.Memory.InjectionServices.Stampers.Serialization.SerializationServices.Core
{
    /// <summary>
    ///     Occurs if no instance of a type can be created. Maybe the type lacks on a public standard (parameterless)
    ///     constructor?
    /// </summary>
    [Serializable]
    internal class CreatingInstanceException : Exception
    {
        ///<summary>
        ///</summary>
        public CreatingInstanceException()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public CreatingInstanceException(string message) : base(message)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CreatingInstanceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected CreatingInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
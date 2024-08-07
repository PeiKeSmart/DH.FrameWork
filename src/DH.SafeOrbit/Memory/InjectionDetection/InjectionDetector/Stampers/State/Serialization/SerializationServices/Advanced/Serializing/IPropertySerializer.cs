﻿//This is a modified version of the beautiful SharpSerializer by Pawel Idzikowski (see: http://www.sharpserializer.com)

using System.IO;
using DG.SafeOrbit.Memory.InjectionServices.Stampers.Serialization.SerializationServices.Core.Property;

namespace DG.SafeOrbit.Memory.InjectionServices.Stampers.Serialization.SerializationServices.Advanced.Serializing
{
    /// <summary>
    ///     Serializes property to a stream
    /// </summary>
    internal interface IPropertySerializer
    {
        /// <summary>
        ///     Open the stream for writing
        /// </summary>
        /// <param name="stream"></param>
        void Open(Stream stream);

        /// <summary>
        ///     Serializes property
        /// </summary>
        /// <param name="property"></param>
        void Serialize(Property property);

        /// <summary>
        ///     Cleaning, but the stream can be used further
        /// </summary>
        void Close();
    }
}
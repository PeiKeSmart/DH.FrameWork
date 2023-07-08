using System;
using System.Reflection;

namespace DG.SafeOrbit.Memory.InjectionServices.Stampers.Serialization.SerializationServices.Common
{
    public static class ReflectionHelper
    {
        public static bool IsType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return type == typeof(Type) || type.GetTypeInfo().IsSubclassOf(typeof(Type));
        }
    }
}
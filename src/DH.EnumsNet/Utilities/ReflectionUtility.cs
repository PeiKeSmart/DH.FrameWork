﻿using System;
using System.Runtime.CompilerServices;
#if !TYPE_REFLECTION || !GET_TYPE_CODE
using System.Collections.Generic;
#endif
#if !TYPE_REFLECTION
using System.Reflection;
#endif

namespace EnumsNET.Utilities;
internal static class ReflectionUtility {
#if !TYPE_REFLECTION
        public static IEnumerable<Attribute> GetCustomAttributes(this Type type, bool inherit) => type.GetTypeInfo().GetCustomAttributes(inherit);

        public static IEnumerable<Type> GetInterfaces(this Type type) => type.GetTypeInfo().ImplementedInterfaces;

        public static bool IsDefined(this Type type, Type attributeType, bool inherit) => type.GetTypeInfo().IsDefined(attributeType, inherit);

        public static bool IsInstanceOfType(this Type type, object obj) => obj != null && type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
#endif

#if !GET_TYPE_CODE
        private static readonly Dictionary<Type, TypeCode> s_typeCodeMap = new Dictionary<Type, TypeCode>(11)
        {
            { typeof(sbyte), TypeCode.SByte },
            { typeof(byte), TypeCode.Byte },
            { typeof(short), TypeCode.Int16 },
            { typeof(ushort), TypeCode.UInt16 },
            { typeof(int), TypeCode.Int32 },
            { typeof(uint), TypeCode.UInt32 },
            { typeof(long), TypeCode.Int64 },
            { typeof(ulong), TypeCode.UInt64 },
            { typeof(string), TypeCode.String },
            { typeof(bool), TypeCode.Boolean },
            { typeof(char), TypeCode.Char }
        };
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TypeCode GetTypeCode(this Type type) =>
#if GET_TYPE_CODE
        Type.GetTypeCode(type);
#else
            s_typeCodeMap.TryGetValue(type, out var typeCode) ? typeCode : TypeCode.Object;
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEnum(this Type type) => type.
#if !TYPE_REFLECTION
            GetTypeInfo().
#endif
        IsEnum;
}
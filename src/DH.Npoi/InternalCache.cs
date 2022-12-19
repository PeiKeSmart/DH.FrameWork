﻿using System;
using System.Collections.Concurrent;
using System.Reflection;
using DH.Npoi.Configurations;

namespace DH.Npoi
{
    internal static class InternalCache
    {
        /// <summary>
        ///     TypeExcelConfigurationCache
        /// </summary>
        public static readonly ConcurrentDictionary<Type, IExcelConfiguration> TypeExcelConfigurationDictionary = new();

        public static readonly ConcurrentDictionary<PropertyInfo, Delegate?> OutputFormatterFuncCache = new();

        public static readonly ConcurrentDictionary<PropertyInfo, Delegate?> InputFormatterFuncCache = new();

        public static readonly ConcurrentDictionary<PropertyInfo, Delegate?> ColumnInputFormatterFuncCache = new();
    }
}

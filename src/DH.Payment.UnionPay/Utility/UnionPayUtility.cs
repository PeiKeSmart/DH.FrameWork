﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace DH.Payment.UnionPay.Utility
{
    /// <summary>
    /// UnionPay 工具类。
    /// </summary>
    public static class UnionPayUtility
    {
        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="dictionary">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> dictionary)
        {
            var content = new StringBuilder();
            foreach (var iter in dictionary)
            {
                if (!string.IsNullOrEmpty(iter.Value))
                {
                    content.Append(iter.Key + "=" + WebUtility.UrlEncode(iter.Value) + "&");
                }
            }
            return content.ToString().Substring(0, content.Length - 1);
        }

        internal static object TryTo<T>(this object Object)
        {
            return Object.TryTo(typeof(T));
        }

        internal static object TryTo(this object Object, Type destinationType)
        {
            try
            {
                if (Object == null || Convert.IsDBNull(Object))
                {
                    return GetDefault(destinationType);
                }

                if (Object as string != null)
                {
                    var ObjectValue = Object as string;
                    if (destinationType.IsEnum)
                    {
                        return Enum.Parse(destinationType, ObjectValue, true);
                    }

                    if (string.IsNullOrEmpty(ObjectValue))
                    {
                        return GetDefault(destinationType);
                    }
                }
                if (Object as IConvertible != null)
                {
                    var destination =
                       destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                           Nullable.GetUnderlyingType(destinationType) : destinationType;
                    return Convert.ChangeType(Object, destination);
                }
                if (destinationType.IsAssignableFrom(Object.GetType()))
                {
                    return Object;
                }

                var Converter = TypeDescriptor.GetConverter(Object.GetType());
                if (Converter.CanConvertTo(destinationType))
                {
                    return Converter.ConvertTo(Object, destinationType);
                }
            }
            catch { }
            return GetDefault(destinationType);
        }

        private static object GetDefault(Type type)
        {
            var defaultExpr = Expression.Default(type);
            return Expression.Lambda<Func<object>>(defaultExpr).Compile()();
        }
    }
}

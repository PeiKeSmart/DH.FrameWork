﻿using System;
using System.Collections.Generic;

namespace DH.Payment.QPay
{
    public class QPayDictionary : SortedDictionary<string, string>
    {
        private const string DATE_TIME_FORMAT = "yyyyMMddHHmmss";

        public QPayDictionary() { }

        public QPayDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        { }

        public void Add(string key, object value)
        {
            string strValue;

            if (value == null)
            {
                strValue = null;
            }
            else if (value is string)
            {
                strValue = (string)value;
            }
            else if (value is DateTime?)
            {
                var dateTime = value as DateTime?;
                strValue = dateTime.Value.ToString(DATE_TIME_FORMAT);
            }
            else if (value is int?)
            {
                strValue = (value as int?).Value.ToString();
            }
            else if (value is long?)
            {
                strValue = (value as long?).Value.ToString();
            }
            else if (value is double?)
            {
                strValue = (value as double?).Value.ToString();
            }
            else if (value is bool?)
            {
                strValue = (value as bool?).Value.ToString().ToLowerInvariant();
            }
            else
            {
                strValue = value.ToString();
            }

            Add(key, strValue);
        }

        public new void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                base.Add(key, value);
            }
        }

        public string GetValue(string key)
        {
            return TryGetValue(key, out var o) ? o : null;
        }
    }
}

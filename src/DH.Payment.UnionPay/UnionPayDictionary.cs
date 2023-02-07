﻿using System.Collections.Generic;

namespace DG.Payment.UnionPay
{
    /// <summary>
    /// UnionPay 字典。
    /// </summary>
    public class UnionPayDictionary : Dictionary<string, string>
    {
        public UnionPayDictionary() { }

        public UnionPayDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        { }

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

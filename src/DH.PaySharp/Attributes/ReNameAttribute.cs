using System;

namespace DH.PaySharp
{
    /// <summary>
    /// 重命名属性
    /// </summary>
    public class ReNameAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

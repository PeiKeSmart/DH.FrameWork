using System;
using DH.Npoi.Settings;

namespace DH.Npoi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class FilterAttribute : Attribute
    {
        public FilterAttribute(int firstColumn) : this(firstColumn, null)
        {
        }

        public FilterAttribute(int firstColumn, int? lastColumn) =>
            FilterSetting = new FilterSetting(firstColumn, lastColumn);

        internal FilterSetting FilterSetting { get; }

        /// <summary>
        ///     Gets or sets the first column index.
        /// </summary>
        public int FirstColumn => FilterSetting.FirstColumn;

        /// <summary>
        ///     Gets or sets the last column index.
        /// </summary>
        public int? LastColumn => FilterSetting.LastColumn;
    }
}

using DH.Extension;

using System.Text;

namespace DH.Extensions;

/// <summary>
/// 数组(<see cref="Array"/>) 扩展
/// </summary>
public static partial class ArrayExtensions
{
    #region WithInIndex(判断索引是否在数组中)

    /// <summary>
    /// 判断索引是否在数组中
    /// </summary>
    /// <param name="source">数组</param>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public static bool WithInIndex(this Array source, int index)
    {
        return source != null && index >= 0 && index < source.Length;
    }

    /// <summary>
    /// 判断索引是否在数组中
    /// </summary>
    /// <param name="source">数组</param>
    /// <param name="index">索引</param>
    /// <param name="dimension">数组维度</param>
    /// <returns></returns>
    public static bool WithInIndex(this Array source, int index, int dimension)
    {
        return source != null && index >= source.GetLowerBound(dimension) &&
               index <= source.GetUpperBound(dimension);
    }

    #endregion

    #region CombineArray(合并数组)

    /// <summary>
    /// 合并数组，合并两个数组到一个新的数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="combineWith">源数组</param>
    /// <param name="arrayToCombine">目标数组</param>
    /// <example>
    /// 	<code>
    /// 		int[] arrayOne = new[] { 1, 2, 3, 4 };
    /// 		int[] arrayTwo = new[] { 5, 6, 7, 8 };
    /// 		Array combinedArray = arrayOne.CombineArray&lt;int&gt;(arrayTwo);
    /// 	</code>
    /// </example>
    /// <returns></returns>
    public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
    {
        if (combineWith != default(T[]) && arrayToCombine != default(T[]))
        {
            int initialSize = combineWith.Length;
            Array.Resize(ref combineWith, initialSize + arrayToCombine.Length);
            Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize,
                arrayToCombine.Length);
        }
        return combineWith;
    }

    #endregion

    #region ClearAll(清空数组内容)

    /// <summary>
    /// 清空数组内容
    /// </summary>
    /// <param name="source">源数组</param>
    /// <returns></returns>
    public static Array ClearAll(this Array source)
    {
        if (source != null)
        {
            Array.Clear(source, 0, source.Length);
        }

        return source;
    }

    /// <summary>
    /// 清空数组内容
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="source">源数组</param>
    /// <returns></returns>
    public static T[] ClearAll<T>(this T[] source)
    {
        if (source != null)
        {
            for (int i = source.GetLowerBound(0); i <= source.GetUpperBound(0); ++i)
            {
                source[i] = default(T);
            }
        }

        return source;
    }

    #endregion

    #region ClearAt(清除数组中指定索引的内容)

    /// <summary>
    /// 清除数组中指定索引的内容
    /// </summary>
    /// <param name="array">数组</param>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public static Array ClearAt(this Array array, int index)
    {
        if (array != null)
        {
            var arrayIndex = index.GetArrayIndex();
            if (arrayIndex.IsIndexInArray(array))
            {
                Array.Clear(array, arrayIndex, 1);
            }
        }

        return array;
    }

    /// <summary>
    /// 清除数组中指定索引的内容
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public static T[] ClearAt<T>(this T[] array, int index)
    {
        if (array != null)
        {
            var arrayIndex = index.GetArrayIndex();
            if (arrayIndex.IsIndexInArray(array))
            {
                array[arrayIndex] = default(T);
            }
        }

        return array;
    }

    #endregion

    #region BlockCopy(复制数据块)

    /// <summary>
    /// 复制数据块，复制数组内容到新数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="source">数据源</param>
    /// <param name="index">索引</param>
    /// <param name="length">复制长度</param>
    /// <returns></returns>
    public static T[] BlockCopy<T>(this T[] source, int index, int length)
    {
        return BlockCopy(source, index, length, false);
    }

    /// <summary>
    /// 复制数据块，复制数组内容到新数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="source">数据源</param>
    /// <param name="index">索引</param>
    /// <param name="length">复制长度</param>
    /// <param name="padToLength">是否填充指定长度</param>
    /// <returns></returns>
    public static T[] BlockCopy<T>(this T[] source, int index, int length, bool padToLength)
    {
        if (source == null)
        {
            throw new NullReferenceException(nameof(source));
        }

        int n = length;
        T[] b = null;
        if (source.Length < index + length)
        {
            n = source.Length - index;// n=source数组剩余长度
            if (padToLength)
            {
                b = new T[length];
            }
        }

        if (b == null)
        {
            b = new T[n];
        }
        Array.Copy(source, index, b, 0, n);// 从source数组指定索引开始复制数据到b数组当中，直至到达指定长度结束复制
        return b;
    }

    /// <summary>
    /// 复制数据块，复制数组内容到新数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="source">数据源</param>
    /// <param name="length">复制长度</param>
    /// <param name="padToLength">是否填充指定长度</param>
    /// <returns></returns>
    public static IEnumerable<T[]> BlockCopy<T>(this T[] source, int length, bool padToLength)
    {
        for (int i = 0; i < source.Length; i += length)
        {
            yield return source.BlockCopy(i, length, padToLength);
        }
    }

    #endregion

    #region 数组操作

    /// <summary>
    /// 获得字符串在字符串数组中的位置
    /// </summary>
    public static int GetIndexInArray(string s, string[] array, bool ignoreCase)
    {
        if (string.IsNullOrEmpty(s) || array == null || array.Length == 0)
            return -1;

        int index = 0;
        string temp = null;

        if (ignoreCase)
            s = s.ToLower();

        foreach (string item in array)
        {
            if (ignoreCase)
                temp = item.ToLower();
            else
                temp = item;

            if (s == temp)
                return index;
            else
                index++;
        }

        return -1;
    }

    /// <summary>
    /// 获得字符串在字符串数组中的位置
    /// </summary>
    public static int GetIndexInArray(string s, string[] array)
    {
        return GetIndexInArray(s, array, false);
    }

    /// <summary>
    /// 判断字符串是否在字符串数组中
    /// </summary>
    public static bool IsInArray(string s, string[] array, bool ignoreCase)
    {
        return GetIndexInArray(s, array, ignoreCase) > -1;
    }

    /// <summary>
    /// 判断字符串是否在字符串数组中
    /// </summary>
    public static bool IsInArray(string s, string[] array)
    {
        return IsInArray(s, array, false);
    }

    /// <summary>
    /// 判断字符串是否在字符串中
    /// </summary>
    public static bool IsInArray(string s, string array, string splitStr, bool ignoreCase)
    {
        return IsInArray(s, array.SplitString(splitStr), ignoreCase);
    }

    /// <summary>
    /// 判断字符串是否在字符串中
    /// </summary>
    public static bool IsInArray(string s, string array, string splitStr)
    {
        return IsInArray(s, array.SplitString(splitStr), false);
    }

    /// <summary>
    /// 判断字符串是否在字符串中
    /// </summary>
    public static bool IsInArray(string s, string array)
    {
        return IsInArray(s, array.SplitString(","), false);
    }



    /// <summary>
    /// 将对象数组拼接成字符串
    /// </summary>
    public static string ObjectArrayToString(object[] array, string splitStr)
    {
        if (array == null || array.Length == 0)
            return "";

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
            result.AppendFormat("{0}{1}", array[i], splitStr);

        return result.Remove(result.Length - splitStr.Length, splitStr.Length).ToString();
    }

    /// <summary>
    /// 将对象数组拼接成字符串
    /// </summary>
    public static string ObjectArrayToString(object[] array)
    {
        return ObjectArrayToString(array, ",");
    }

    /// <summary>
    /// 将字符串数组拼接成字符串
    /// </summary>
    public static string StringArrayToString(string[] array, string splitStr)
    {
        return ObjectArrayToString(array, splitStr);
    }

    /// <summary>
    /// 将字符串数组拼接成字符串
    /// </summary>
    public static string StringArrayToString(string[] array)
    {
        return StringArrayToString(array, ",");
    }

    /// <summary>
    /// 将整数数组拼接成字符串
    /// </summary>
    public static string IntArrayToString(int[] array, string splitStr)
    {
        if (array == null || array.Length == 0)
            return "";

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
            result.AppendFormat("{0}{1}", array[i], splitStr);

        return result.Remove(result.Length - splitStr.Length, splitStr.Length).ToString();
    }

    /// <summary>
    /// 将整数数组拼接成字符串
    /// </summary>
    public static string IntArrayToString(int[] array)
    {
        return IntArrayToString(array, ",");
    }



    /// <summary>
    /// 移除数组中的指定项
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="removeItem">要移除的项</param>
    /// <param name="removeBackspace">是否移除空格</param>
    /// <param name="ignoreCase">是否忽略大小写</param>
    /// <returns></returns>
    public static string[] RemoveArrayItem(string[] array, string removeItem, bool removeBackspace, bool ignoreCase)
    {
        if (array != null && array.Length > 0)
        {
            StringBuilder arrayStr = new StringBuilder();
            if (ignoreCase)
                removeItem = removeItem.ToLower();
            string temp = "";
            foreach (string item in array)
            {
                if (ignoreCase)
                    temp = item.ToLower();
                else
                    temp = item;

                if (temp != removeItem)
                    arrayStr.AppendFormat("{0}_", removeBackspace ? item.Trim() : item);
            }

            return arrayStr.Remove(arrayStr.Length - 1, 1).ToString().SplitString("_");
        }

        return array;
    }

    /// <summary>
    /// 移除数组中的指定项
    /// </summary>
    /// <param name="array">源数组</param>
    /// <returns></returns>
    public static string[] RemoveArrayItem(string[] array)
    {
        return RemoveArrayItem(array, "", true, false);
    }

    /// <summary>
    /// 移除字符串中的指定项
    /// </summary>
    /// <param name="s">源字符串</param>
    /// <param name="splitStr">分割字符串</param>
    /// <returns></returns>
    public static string[] RemoveStringItem(string s, string splitStr)
    {
        return RemoveArrayItem(s.SplitString(splitStr), "", true, false);
    }

    /// <summary>
    /// 移除字符串中的指定项
    /// </summary>
    /// <param name="s">源字符串</param>
    /// <returns></returns>
    public static string[] RemoveStringItem(string s)
    {
        return RemoveArrayItem(s.SplitString(), "", true, false);
    }



    /// <summary>
    /// 移除数组中的重复项
    /// </summary>
    /// <returns></returns>
    public static int[] RemoveRepeaterItem(int[] array)
    {
        if (array == null || array.Length < 2)
            return array;

        Array.Sort(array);

        int length = 1;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] != array[i - 1])
                length++;
        }

        int[] uniqueArray = new int[length];
        uniqueArray[0] = array[0];
        int j = 1;
        for (int i = 1; i < array.Length; i++)
            if (array[i] != array[i - 1])
                uniqueArray[j++] = array[i];

        return uniqueArray;
    }

    /// <summary>
    /// 移除数组中的重复项
    /// </summary>
    /// <returns></returns>
    public static string[] RemoveRepeaterItem(string[] array)
    {
        if (array == null || array.Length < 2)
            return array;

        Array.Sort(array);

        int length = 1;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] != array[i - 1])
                length++;
        }

        string[] uniqueArray = new string[length];
        uniqueArray[0] = array[0];
        int j = 1;
        for (int i = 1; i < array.Length; i++)
            if (array[i] != array[i - 1])
                uniqueArray[j++] = array[i];

        return uniqueArray;
    }

    /// <summary>
    /// 去除字符串中的重复元素
    /// </summary>
    /// <returns></returns>
    public static string GetUniqueString(string s)
    {
        return GetUniqueString(s, ",");
    }

    /// <summary>
    /// 去除字符串中的重复元素
    /// </summary>
    /// <returns></returns>
    public static string GetUniqueString(string s, string splitStr)
    {
        return ObjectArrayToString(RemoveRepeaterItem(s.SplitString(splitStr)), splitStr);
    }

    #endregion
}
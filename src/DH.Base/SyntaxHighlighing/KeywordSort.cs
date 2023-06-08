using System.Collections;
using System.Text;

namespace DH.SyntaxHighlighing;

/// <summary>
/// 渲染HTML页面关键词高亮分组排序算法
/// </summary>
public class KeywordSort {
    /// <summary>
      /// 关键词分组排序算法
      /// </summary>
      /// <param name="keyWords"></param>
      /// <returns></returns>
    public static string SortKeywords(string keyWords)
    {
        try
        {
            #region 按每个关键词包含关系分组

            SortedList groups = new();
            string[] keys = keyWords.Split(',');
            foreach (string key in keys)
            {
                if (String.IsNullOrEmpty(key)) continue;

                ArrayList sort = new();
                foreach (string s in keys)
                {
                    if (s.IndexOf(key) >= 0) sort.Add(s);
                }
                if (sort.Count > 1) sort.Sort(new SortComapre());//按包含关系排序
                groups.Add(key, sort);
            }

            #endregion

            #region 重新组合关键词 - 按关键词分组关系重新组合

            ArrayList result = new();

            foreach (DictionaryEntry g in groups)
            {
                foreach (string s in (g.Value as ArrayList))
                {
                    if (!result.Contains(s)) result.Add(s);
                }
            }

            #endregion

            //输出关键词组合，逗号分开
            StringBuilder sb = new();
            foreach (string s in result) sb.Append(s + ",");

            return sb.ToString();
        }
        catch (Exception)
        {
            return keyWords;
        }
    }
}
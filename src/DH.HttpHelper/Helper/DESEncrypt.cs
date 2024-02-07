using System;
using System.Security.Cryptography;
using System.Text;

namespace DG.HttpHelper.Helper
{
    /// <summary>
    /// DES加密/解密类。
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DESEncrypt
    {
        public DESEncrypt()
        {
        }

        #region ========加密======== 

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "SufeiNet");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">要加载的数据</param> 
        /// <param name="sKey">Key</param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5By32(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5By32(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密======== 

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "SufeiNet");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">要解密的数据</param> 
        /// <param name="sKey">key</param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5By32(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5By32(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region Md5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="value">值</param>
        public static string Md5By16(string value) => Md5By16(value, Encoding.UTF8);

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By16(string value, Encoding encoding) => Md5(value, encoding, 4, 8);

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="value">值</param>
        public static string Md5By32(string value) => Md5By32(value, Encoding.UTF8);

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By32(string value, Encoding encoding) => Md5(value, encoding, null, null);

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        private static string Md5(string value, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                var hash = md5.ComputeHash(encoding.GetBytes(value));
                result = startIndex == null
                    ? BitConverter.ToString(hash)
                    : BitConverter.ToString(hash, startIndex.Value, length.Value);
            }
            finally
            {
                md5?.Clear();
                md5?.Dispose();
            }
            return result.Replace("-", "");
        }

        #endregion
    }
}

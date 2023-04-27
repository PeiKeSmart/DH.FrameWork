﻿using DH.Helpers;

using System.Security.Cryptography;
using System.Text;

namespace DH.Security {
    /// <summary>
    /// 加密操作
    /// 说明：
    /// 1、AES加密整理自支付宝SDK
    /// 2、RSA加密采用 https://github.com/stulzq/DotnetCore.RSA/blob/master/DotnetCore.RSA/RSAHelper.cs
    /// </summary>
    public static partial class Encrypt
    {
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
            var md5 = MD5.Create();
            string result;
            try
            {
                var hash = md5.ComputeHash(encoding.GetBytes(value));
                result = startIndex == null
                    ? BitConverter.ToString(hash)
                    : BitConverter.ToString(hash, startIndex.SafeValue(), length.SafeValue());
            }
            finally
            {
                md5?.Clear();
                md5?.Dispose();
            }
            return result.Replace("-", "");
        }

        /// <summary>
        ///     对字符串进行MD5加密
        /// </summary>
        /// <param name="message">需要加密的字符串</param>
        /// <returns>加密后的结果</returns>
        public static string MDString(this string message)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.Default.GetBytes(message);
            byte[] bytes = md5.ComputeHash(buffer);
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        /// <summary>
        ///     对字符串进行MD5二次加密
        /// </summary>
        /// <param name="message">需要加密的字符串</param>
        /// <returns>加密后的结果</returns>
        public static string MDString2(this string message) => MDString(MDString(message));

        /// <summary>
        /// MD5 三次加密算法
        /// </summary>
        /// <param name="s">需要加密的字符串</param>
        /// <returns>MD5字符串</returns>
        public static string MDString3(this string s)
        {
            using MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);
            return bytes3.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        /// <summary>
        ///     对字符串进行MD5加盐加密
        /// </summary>
        /// <param name="message">需要加密的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>加密后的结果</returns>
        public static string MDString(this string message, string salt) => MDString(message + salt);

        /// <summary>
        ///     对字符串进行MD5二次加盐加密
        /// </summary>
        /// <param name="message">需要加密的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>加密后的结果</returns>
        public static string MDString2(this string message, string salt) => MDString(MDString(message + salt), salt);

        /// <summary>
        /// MD5 三次加密算法
        /// </summary>
        /// <param name="s">需要加密的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5字符串</returns>
        public static string MDString3(this string s, string salt)
        {
            using MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s + salt);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);
            return bytes3.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        #endregion

        #region DES加密

        /// <summary>
        /// DES密钥，24位字符串
        /// </summary>
        public static string DesKey { get; set; } = "#s^un2ye21fcv%|f0XpR,+vh";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        public static string DesEncrypt(object value) => DesEncrypt(value, DesKey);

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥，24位</param>
        public static string DesEncrypt(object value, string key) => DesEncrypt(value, key, Encoding.UTF8);

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <param name="encoding">字符编码</param>
        public static string DesEncrypt(object value, string key, Encoding encoding)
        {
            var text = value.SafeString();
            if (ValidateDes(text, key) == false)
                return string.Empty;
            using (var transform = CreateDesProvider(key).CreateEncryptor())
                return GetEncryptResult(text, encoding, transform);
        }

        /// <summary>
        /// 验证Des加密参数
        /// </summary>
        /// <param name="text">待加密的文本</param>
        /// <param name="key">密钥，24位</param>
        private static bool ValidateDes(string text, string key) => !string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(key) && key.Length == 24;

        /// <summary>
        /// 创建Des加密服务提供程序
        /// </summary>
        /// <param name="key">密钥，24位</param>
        private static SymmetricAlgorithm CreateDesProvider(string key) 
        {
            SymmetricAlgorithm sma = TripleDES.Create();
            sma.Key = Encoding.ASCII.GetBytes(key);
            sma.Mode = CipherMode.ECB;
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }

        /// <summary>
        /// 获取加密结果
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="transform">加密器</param>
        private static string GetEncryptResult(string value, Encoding encoding, ICryptoTransform transform)
        {
            var bytes = encoding.GetBytes(value);
            var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        public static string DesDecrypt(object value) => DesDecrypt(value, DesKey);

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥，24位</param>
        public static string DesDecrypt(object value, string key) => DesDecrypt(value, key, Encoding.UTF8);

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <param name="encoding">字符编码</param>
        public static string DesDecrypt(object value, string key, Encoding encoding)
        {
            var text = value.SafeString();
            if (!ValidateDes(text, key))
                return string.Empty;
            using (var transform = CreateDesProvider(key).CreateDecryptor())
                return GetDecryptResult(text, encoding, transform);
        }

        /// <summary>
        /// 获取解密结果
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="transform">加密器</param>
        private static string GetDecryptResult(string value, Encoding encoding, ICryptoTransform transform)
        {
            var bytes = Convert.FromBase64String(value);
            var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return encoding.GetString(result);
        }

        #endregion

        #region AES加密

        /// <summary>
        /// 128位0向量
        /// </summary>
        private static byte[] _iv;

        /// <summary>
        /// 128位0向量
        /// </summary>
        private static byte[] Iv
        {
            get
            {
                if (_iv == null)
                {
                    var size = 16;
                    _iv = new byte[size];
                    for (int i = 0; i < size; i++)
                    {
                        _iv[i] = 0;
                    }
                }
                return _iv;
            }
        }

        /// <summary>
        /// AES密钥
        /// </summary>
        public static string AesKey { get; set; } = "QaP1AF8utIarcBqdhYTZpVGbiNQ9M6IL";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        public static string AesEncrypt(string value) => AesEncrypt(value, AesKey);

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        public static string AesEncrypt(string value, string key) => AesEncrypt(value, key, Encoding.UTF8);

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string AesEncrypt(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var rijndaelManaged = CreateRijndaelManaged(key);
            using (var transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV))
                return GetEncryptResult(value, encoding, transform);
        }

        /// <summary>
        /// 创建RijndaelManaged
        /// </summary>
        /// <param name="key">密钥</param>
        private static SymmetricAlgorithm CreateRijndaelManaged(string key)
        {
            SymmetricAlgorithm sma = Aes.Create();
            sma.Key = Convert.FromBase64String(key);
            sma.Mode = CipherMode.CBC;
            sma.Padding = PaddingMode.PKCS7;
            sma.IV = Iv;

            return sma;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        public static string AesDecrypt(string value) => AesDecrypt(value, AesKey);

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥</param>
        public static string AesDecrypt(string value, string key) => AesDecrypt(value, key, Encoding.UTF8);

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string AesDecrypt(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var rijndaelManaged = CreateRijndaelManaged(key);
            using (var transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV))
                return GetDecryptResult(value, encoding, transform);
        }

        #endregion

        #region RSA签名

        /// <summary>
        /// RSA签名，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        public static string RsaSign(string value, string key) => RsaSign(value, key, Encoding.UTF8);

        /// <summary>
        /// RSA签名，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        public static string RsaSign(string value, string key, Encoding encoding) => RsaSign(value, key, encoding, RSAType.RSA);

        /// <summary>
        /// RSA签名，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        public static string Rsa2Sign(string value, string key) => Rsa2Sign(value, key, Encoding.UTF8);

        /// <summary>
        /// RSA签名，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        public static string Rsa2Sign(string value, string key, Encoding encoding) => RsaSign(value, key, encoding, RSAType.RSA2);

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="type">RSA算法类型</param>
        private static string RsaSign(string value, string key, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var rsa = new RsaHelper(type, encoding, key);
            return rsa.Sign(value);
        }

        /// <summary>
        /// RSA验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        public static bool RsaVerify(string value, string publicKey, string sign) => RsaVerify(value, publicKey, sign, Encoding.UTF8);

        /// <summary>
        /// RSA验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        public static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding) => RsaVerify(value, publicKey, sign, encoding, RSAType.RSA);

        /// <summary>
        /// RSA验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        public static bool Rsa2Verify(string value, string publicKey, string sign) => Rsa2Verify(value, publicKey, sign, Encoding.UTF8);

        /// <summary>
        /// RSA验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        public static bool Rsa2Verify(string value, string publicKey, string sign, Encoding encoding) => RsaVerify(value, publicKey, sign, encoding, RSAType.RSA2);

        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="type">RSA算法类型</param>
        private static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            var rsa = new RsaHelper(type, encoding, publicKey: publicKey);
            return rsa.Verify(value, sign);
        }

        #endregion

        #region HmacMd5加密

        /// <summary>
        /// HMACMD5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        public static string HmacMd5(string value, string key) => HmacMd5(value, key, Encoding.UTF8);

        /// <summary>
        /// HMACMD5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string HmacMd5(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var md5 = new HMACMD5(encoding.GetBytes(key));
            var hash = md5.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha1加密

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        public static string HmacSha1(string value, string key) => HmacSha1(value, key, Encoding.UTF8);

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string HmacSha1(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var sha1 = new HMACSHA1(encoding.GetBytes(key));
            var hash = sha1.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha256加密

        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        public static string HmacSha256(string value, string key) => HmacSha256(value, key, Encoding.UTF8);

        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string HmacSha256(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var sha256 = new HMACSHA256(encoding.GetBytes(key));
            var hash = sha256.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha384加密

        /// <summary>
        /// HMACSHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        public static string HmacSha384(string value, string key) => HmacSha384(value, key, Encoding.UTF8);

        /// <summary>
        /// HMACSHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string HmacSha384(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var sha384 = new HMACSHA384(encoding.GetBytes(key));
            var hash = sha384.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha512加密

        /// <summary>
        /// HMACSHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        public static string HmacSha512(string value, string key) => HmacSha512(value, key, Encoding.UTF8);

        /// <summary>
        /// HMACSHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        public static string HmacSha512(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var sha512 = new HMACSHA512(encoding.GetBytes(key));
            var hash = sha512.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region SHA1加密

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="value">值</param>
        public static string Sha1(string value) => Sha1(value, Encoding.UTF8);

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Sha1(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region SHA256加密

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="value">值</param>
        public static string Sha256(string value) => Sha256(value, Encoding.UTF8);

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Sha256(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region SHA384加密

        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="value">值</param>
        public static string Sha384(string value) => Sha384(value, Encoding.UTF8);

        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Sha384(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            using (var sha = SHA384.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region SHA512加密

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="value">值</param>
        public static string Sha512(string value) => Sha512(value, Encoding.UTF8);

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Sha512(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            using (var sha = SHA512.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region Base64加密

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="value">值</param>
        public static string Base64Encrypt(string value) => Base64Encrypt(value, Encoding.UTF8);

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Base64Encrypt(string value, Encoding encoding) => string.IsNullOrWhiteSpace(value) ? string.Empty : Convert.ToBase64String(encoding.GetBytes(value));

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="value">值</param>
        public static string Base64Decrypt(string value) => Base64Decrypt(value, Encoding.UTF8);

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Base64Decrypt(string value, Encoding encoding) =>
            string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : encoding.GetString(Convert.FromBase64String(value));

        #endregion

        #region 获取文件的MD5值

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName">需要求MD5值的文件的文件名及路径</param>
        /// <returns>MD5字符串</returns>
        public static string MDFile(this string fileName)
        {
            using var fs = new BufferedStream(File.Open(fileName, FileMode.Open, FileAccess.Read), 1048576);
            using MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(fs);
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        /// <summary>
        /// 计算文件的sha256
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string SHA256File(this Stream stream)
        {
            using var fs = new BufferedStream(stream, 1048576);
            SHA256Managed sha = new SHA256Managed();
            byte[] checksum = sha.ComputeHash(fs);
            return BitConverter.ToString(checksum).Replace("-", string.Empty);
        }

        /// <summary>
        /// 获取数据流的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>MD5字符串</returns>
        public static string MDString(this Stream stream)
        {
            using var fs = new BufferedStream(stream, 1048576);
            using MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(fs);
            var mdstr = bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
            stream.Position = 0;
            return mdstr;
        }

        #endregion
    }
}

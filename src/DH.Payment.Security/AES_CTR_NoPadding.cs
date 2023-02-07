﻿using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace DH.Payment.Security
{
    public static class AES_CTR_NoPadding
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            return cipher.DoFinal(data);
        }

        public static string Encrypt(string data, string key, byte[] iv)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var encData = Encrypt(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key), iv);
            return Convert.ToBase64String(encData);
        }

        public static string Decrypt(string data, string key, byte[] iv)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            var decData = Decrypt(Convert.FromBase64String(data), Encoding.UTF8.GetBytes(key), iv);
            return Encoding.UTF8.GetString(decData);
        }
    }
}

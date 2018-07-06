using System;
using System.Security.Cryptography;
using System.Text;

namespace Netnr.Core
{
    public class CalcTo
    {
        #region 异或算法

        /// <summary>
        /// 异或算法
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="kye">异或因子 2-253</param>
        /// <returns>返回异或后的字符串</returns>
        public static string XorKey(string s, int key)
        {
            int n = key > 253 ? 253 : key < 2 ? 2 : key;
            byte k = byte.Parse(n.ToString());

            byte[] bytes = Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ k ^ (k + 7));
            }
            return Encoding.Unicode.GetString(bytes);
        }

        #endregion

        #region MD5 加密

        /// <summary>
        /// MD5加密 小写
        /// </summary>
        /// <param name="s">需加密的字符串</param>
        /// <param name="len">长度 默认32 可选16</param>
        /// <returns></returns>
        public static string MD5(string s, int len = 32)
        {
            string result = "";

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            result = sb.ToString();

            //result = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
            return len == 32 ? result : result.Substring(8, 16);
        }

        #endregion

        #region DES 加解密

        #region DES 加密

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string EnDES(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
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

        #region DES 解密

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DeDES(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = System.Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #endregion

        #region SHA1 加密
        //20字节,160位
        public static string SHA128(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteArr = SHA1.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        //32字节,256位
        public static string SHA256(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider();
            byte[] byteArr = SHA256.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        //48字节,384位
        public static string SHA384(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA384CryptoServiceProvider SHA384 = new SHA384CryptoServiceProvider();
            byte[] byteArr = SHA384.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }

        //64字节,512位
        public static string SHA512(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider();
            byte[] byteArr = SHA512.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }

        #endregion

        #region HMAC_SHA1 加密
        public static string HMAC_SHA1(string str, string key)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1
            {
                Key = Encoding.UTF8.GetBytes(key)
            };
            byte[] dataBuffer = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        #endregion
    }
}
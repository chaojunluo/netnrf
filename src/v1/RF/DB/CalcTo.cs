using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DB
{
    public class CalcTo
    {
        #region 异或运算、MD5

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
        
        #region hash加密解密

        /// <summary>
        /// hash加密
        /// </summary>
        /// <param name="s">需加密的字符串</param>
        /// <returns></returns>
        public static string EnHash(string s)
        {
            if (s.Length == 0)
            {
                return "";
            }
            byte[] HaKey = System.Text.Encoding.ASCII.GetBytes(s.ToCharArray());
            byte[] HaData = new byte[20];
            HMACSHA1 Hmac = new HMACSHA1(HaKey);
            CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
            try
            {
                cs.Write(HaData, 0, HaData.Length);
            }
            finally
            {
                cs.Close();
            }
            string HaResult = System.Convert.ToBase64String(Hmac.Hash).Substring(0, 16);
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(HaResult.ToCharArray());
            byte[] RiDataBuf = System.Text.Encoding.ASCII.GetBytes(s.ToCharArray());
            byte[] EncodedBytes = { };
            MemoryStream ms = new MemoryStream();
            RijndaelManaged rv = new RijndaelManaged();
            cs = new CryptoStream(ms, rv.CreateEncryptor(RiKey, RiKey), CryptoStreamMode.Write);
            try
            {
                cs.Write(RiDataBuf, 0, RiDataBuf.Length);
                cs.FlushFinalBlock();
                EncodedBytes = ms.ToArray();
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            return HaResult + System.Convert.ToBase64String(EncodedBytes);
        }

        /// <summary>
        /// hash解密
        /// </summary>
        /// <param name="s">需解密的字符串</param>
        /// <returns></returns>
        public static string DeHash(string s)
        {
            if (s.Length < 40) return "";
            byte[] SrcBytes = System.Convert.FromBase64String(s.Substring(16));
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(s.Substring(0, 16).ToCharArray());
            byte[] InitialText = new byte[SrcBytes.Length];
            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(SrcBytes);
            CryptoStream cs = new CryptoStream(ms, rv.CreateDecryptor(RiKey, RiKey), CryptoStreamMode.Read);
            try
            {
                cs.Read(InitialText, 0, InitialText.Length);
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            for (int i = 0; i < InitialText.Length; ++i) if (InitialText[i] > 0) Result.Append((char)InitialText[i]);
            return Result.ToString();
        }

        #endregion
    }
}
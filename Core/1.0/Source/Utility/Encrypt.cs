using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Cdts.Utility
{
    public static class Encrypt
    {
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="obj">明文</param>
        /// <returns>返回密文</returns>
        public static string SHA256(this string obj)
        {
            byte[] data = Encoding.UTF8.GetBytes(obj);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(data);
            return Convert.ToBase64String(result);
        }

        public static string CdtsEncode(this string obj)
        {
            string result = "";
            int fa = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                fa = (int)obj[i] + Functions.Fibonacci(i + 1);
                while (fa > 126) { fa -= 94; }
                result += (char)(fa);
            }
            return result;
        }
        public static string CdtsDecode(this string obj)
        {
            string result = "";
            int fa = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                fa = (int)obj[i] - Functions.Fibonacci(i + 1);
                while (fa <33) { fa += 94; }
                result += (char)(fa);
            }
            return result;
        }
    }
}

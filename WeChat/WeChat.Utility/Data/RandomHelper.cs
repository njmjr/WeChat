using System;
using System.Text;

namespace WeChat.Utility.Data
{
    public static class RandomHelper
    {
        private static String GenerateString(String from, int length)
        {
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(from[rnd.Next(from.Length)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成随机字符串. (A-Za-z0-9)
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String GenerateString(int length)
        {
            return GenerateString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length);
        }
    }
}

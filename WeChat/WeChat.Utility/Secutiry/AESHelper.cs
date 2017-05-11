using System;
using System.IO;
using System.Security.Cryptography;

namespace WeChat.Utility.Secutiry
{
    /// <summary>
    /// 变态的AES加密
    /// 加密块长度16，密钥长度32，初始化向量{0}^16,补充模式Zeros,加密方式ECB
    /// dongx
    /// 20151010
    /// </summary>
    public static class AesHelper
    {
        private static readonly byte[] Key =
        { 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31,
            0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31 };
        private static readonly byte[] Iv = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string toEncrypt)
        {
            if(string.IsNullOrEmpty(toEncrypt))
            {
                return string.Empty;
            }
            RijndaelManaged rijalg = new RijndaelManaged();
            rijalg.BlockSize = 128;
            rijalg.KeySize = 256; 
            rijalg.Padding = PaddingMode.Zeros;
            rijalg.Mode = CipherMode.ECB;

            rijalg.Key = Key;
            rijalg.IV = Iv;
            ICryptoTransform encryptor = rijalg.CreateEncryptor(rijalg.Key, rijalg.IV);

            byte[] encrypted;
            byte[] inputByteArray = System.Text.Encoding.Default.GetBytes(toEncrypt);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(inputByteArray, 0, inputByteArray.Length);
                    csEncrypt.FlushFinalBlock();
                    encrypted = msEncrypt.ToArray();//得到加密后的字节数组   
                    csEncrypt.Close();
                    msEncrypt.Close();    
                }
            }
            return ByteToString(encrypted);
        }
         
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptStr">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encryptStr)
        {
            if (string.IsNullOrEmpty(encryptStr))
            {
                return string.Empty;
            }
            RijndaelManaged rijalg = new RijndaelManaged();
            rijalg.BlockSize = 128;
            rijalg.KeySize = 256;
            rijalg.Padding = PaddingMode.Zeros;
            rijalg.Mode = CipherMode.ECB;

            rijalg.Key = Key;
            rijalg.IV = Iv;
            ICryptoTransform encryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);
            byte[] inputByteArray = StrToToHexByte(encryptStr);
            byte[] decryptBytes = new byte[inputByteArray.Length];
            using (MemoryStream msEncrypt = new MemoryStream(inputByteArray))
            {
                using (CryptoStream cs = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    msEncrypt.Close();
                }
            }
            return System.Text.Encoding.Default.GetString(decryptBytes).Replace("\u0000", String.Empty).Trim();
        }

        private static string ByteToString(byte[] inBytes)
        {
            string stringOut = "";
            foreach (byte inByte in inBytes)
            {
                stringOut = stringOut + String.Format("{0:X2}", inByte);
            }
            return stringOut;
        }

        private static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        } 



    }
}

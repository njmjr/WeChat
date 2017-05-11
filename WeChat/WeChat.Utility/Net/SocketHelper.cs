using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WeChat.Utility.Extensions;
using WeChat.Utility.Secutiry;

namespace WeChat.Utility.Net
{
    public class SocketHelper
    {
        public static string GetMac2(string asn, string cardTradeNo, int permoney, int tradeMoney, string tradeDate, string tradeTime, string termNo, string ranNum, string mac)
        {
            string mac2;
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Mac2SocketServerHost"]))
            {
                mac2 = "FFFFFFFF";
            }
            else
            {
                IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["Mac2SocketServerHost"]);
                IPEndPoint ipe = new IPEndPoint(ip, Convert.ToInt32(ConfigurationManager.AppSettings["Mac2SocketServerPort"]));
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    clientSocket.Connect(ipe);
                }
                catch (Exception)
                {
                    throw new WeChatException("MAC2_ERROR", "生成MAC2失败");
                }
                if (asn.Length > 16)
                {
                    asn = asn.Right(16);
                }
                string permoney16X = permoney.ToString("x8");
                string tradeMoney16X = tradeMoney.ToString("x8");
                string sendStr = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}", "2061", "1", asn, cardTradeNo.PadLeft(6, '0'), permoney16X.PadLeft(8, '0'), tradeMoney16X.PadLeft(8, '0'),
                   tradeDate, tradeTime, "02", termNo.PadLeft(12, '0'), ranNum.PadLeft(8, '0'), mac.PadLeft(8, '0'), "".PadLeft(64, '0'));
                byte[] sendBytes = Encoding.ASCII.GetBytes(AesHelper.Encrypt(sendStr));
                clientSocket.Send(sendBytes);
                string recStr = "";
                byte[] recBytes = new byte[4096];
                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
                recStr = AesHelper.Decrypt(recStr);
                if (string.IsNullOrEmpty(recStr) || recStr.Length < 79 || recStr.Substring(0, 4) != "0000")
                {
                    throw new WeChatException("MAC2_ERROR", "生成MAC2失败:" + recStr);
                }
                mac2 = recStr.Substring(79, 8);
                clientSocket.Close();
            }
            return mac2;
        }
    }
}

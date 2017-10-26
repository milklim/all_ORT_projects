using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace calcTcpIp
{
    class Program
    {
        const int port = 8888;
        const string header = "HTTP/1.1 200 OK\n" +
                    "Access-Control-Allow-Origin: * \n" +
                    "Connection: close\n" +
                    "Content-Type: text/plain\n\n";  
        static void Main(string[] args)
        {
            double num1 = 0,
                   num2 = 0;
            string opr = "",
                   res = "";
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                server.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    string message = GetData(stream);

                    Console.WriteLine(">>> {0}\n", message);

                    var query = ParseParams(message);
                    bool isNum = double.TryParse(query["num1"], out num1);
                    isNum = double.TryParse(query["num2"], out num2) && isNum;
                    opr = query["opr"];

                    if (isNum)
                    {
                        res = Calulate(num1, num2, opr);
                    }
                    else
                    {
                        res = "Enter correct values of num1 & num2";
                    }

                    using (BinaryWriter bw = new BinaryWriter(stream))
                    {
                        bw.Write(header + res);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }

        }

        private static string Calulate(double num1, double num2, string opr)
        {
            string res;
            switch (opr)
            {
                case "+":
                case "plus":
                    res = (num1 + num2).ToString();
                    break;
                case "-":
                    res = (num1 - num2).ToString();
                    break;
                case "/":
                    if (num2 != 0) { res = (num1 / num2).ToString(); }
                    else { res = "Err. Division by zero."; }
                    break;
                case "*":
                    res = (num1 * num2).ToString();
                    break;
                default:
                    res = "Err. Operation not supported";
                    break;
            }
            return res;
        }

        private static string GetData(NetworkStream stream)
        {
            byte[] data = new byte[512]; 
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        private static Dictionary<string, string> ParseParams(string msg)
        {
            string req = msg;
            Dictionary<string, string> paramsDict = new Dictionary<string, string>();

            if (req.StartsWith("GET"))
            {
                int startPos = req.IndexOf('?') + 1;
                int endPos = req.IndexOf(" HTTP");
                req = req.Substring(startPos, endPos - startPos);

                foreach (var item in req.Split('&'))
                {
                    string[] tmpValues = item.Split('=');
                    paramsDict.Add(tmpValues[0], string.IsNullOrEmpty(tmpValues[1]) ? "" : tmpValues[1]);
                }
            }

            if (req.StartsWith("POST"))
            {
                int startPos = req.IndexOf("\r\n\r\n") + 4;
                req = req.Substring(startPos);

                foreach (var item in req.Split('&'))
                {
                    string[] tmpValues = item.Split('=');
                    paramsDict.Add(tmpValues[0], string.IsNullOrEmpty(tmpValues[1]) ? "" : tmpValues[1]);
                }
            }
            return paramsDict;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketChat
{
    class Program
    {
        static void Main(string[] args)
        {
            WSServer wsServer = new WSServer("http://localhost:8080/");
            wsServer.Start();
            Console.ReadLine();
        }
    }
}

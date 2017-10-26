using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading;


namespace WebSocketChat
{
    class WSServer
    {
        HttpListener httpListener;
        List<WebSocket> clients;

        public WSServer(string uriPrefix)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(uriPrefix);
            clients = new List<WebSocket>();
        }

        public async void Start()
        {
            httpListener.Start();
            Console.WriteLine("Working...");

            while (true)
            {
                HttpListenerContext httpListenerContext = await httpListener.GetContextAsync();

                if (httpListenerContext.Request.IsWebSocketRequest)
                    ProcessRequest(httpListenerContext);
                else
                    httpListenerContext.Response.Close();
            }
        }

        private async void ProcessRequest(HttpListenerContext context)
        {
            WebSocketContext webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
            WebSocket webSocket = webSocketContext.WebSocket;

            if (clients.Contains(webSocket) == false)
                clients.Add(webSocket);

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = new byte[1024];
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                        CancellationToken.None);

                    foreach (WebSocket socket in clients)
                    {

                        if (socket != webSocket)
                        {
                            await socket.SendAsync(new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                                             WebSocketMessageType.Text, receiveResult.EndOfMessage, CancellationToken.None); 
                        }
                    }
                }
            }
            catch
            {
                webSocket.Dispose();
                clients.Remove(webSocket);
            }
        }
    }
}

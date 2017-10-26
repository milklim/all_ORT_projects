using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        static ClientWebSocket client;

        public Form1()
        {
            InitializeComponent();    
        }

        async private void InitChatClient()
        {
            try
            {

                client = new ClientWebSocket();
                await client.ConnectAsync(new Uri("ws://localhost:8080"), CancellationToken.None);
                this.textBox_chatBox.AppendText($"\r\n>>> Вы подключились к чату");
                ReceiveMessage();
            }
            catch (Exception)
            {

                this.textBox_chatBox.AppendText($"\r\n>>> Не удалось подключиться к серверу.");
            }

        }

        async private void ReceiveMessage()
        {
            while (client.State == WebSocketState.Open)
            {
                byte[] buffer = new byte[1024];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string msg = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                this.textBox_chatBox.AppendText($"\r\n>>> {msg}");
            }
        }


        async private void SendMessage(string msg)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, false, CancellationToken.None);
                this.textBox_chatBox.AppendText($"\r\n<<< {msg}");
            }
            catch
            {
                this.textBox_chatBox.AppendText($"\r\n>>> Не удалось подключиться к серверу.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMsg();
        }

        private void SendMsg()
        {
            SendMessage(this.textBox_input.Text);
            this.textBox_input.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                InitChatClient();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void textBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMsg();

            }
        }
    }
}

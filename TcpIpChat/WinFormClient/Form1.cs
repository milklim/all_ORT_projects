using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        static string userName = "NoName";
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        public Form1()
        {
            InitializeComponent();
           
        }

        public void InitChatClient()
        {
            this.Text += " - " + Form1.userName;

            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                this.textBox_chatBox.AppendText($"Добро пожаловать, {userName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    this.textBox_chatBox.Invoke(new Action(() =>
                                { textBox_chatBox.Text += string.Format("\r\n{0}", message); }));
                }
                catch
                {
                    break;
                }
        
            }
        }

        private void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
           // Environment.Exit(0); //завершение процесса
        }

        private void SendMessage(string msg)
        {
            try
            {
                this.textBox_chatBox.AppendText($"\r\n>>> {msg}");
                byte[] data = Encoding.Unicode.GetBytes(msg);
                stream.Write(data, 0, data.Length);
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

            UserNameDialog userNameDialog = new UserNameDialog();
            userNameDialog.ShowDialog();

            if (userNameDialog.DialogResult == DialogResult.OK)
            {
                Form1.userName = userNameDialog.UsrName;
                InitChatClient();
            }
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

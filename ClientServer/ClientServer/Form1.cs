using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Server.Utils;
using System.Collections.Generic;

namespace ClientServer
{
    public partial class ClientForm : Form
    {
        //константы
        public const string CRLF = "\r\n";
        public const string LOCALHOST = "127.0.0.1";
        //private const string LOCALHOST = "192.168.1.71"; //проверял на работе в домашней сети между компьютерами
        public const int DEFAULT_PORT = 5959;

        private FilesHandler _filesHandler;
        private TcpClient _client;

        public ClientForm()
        {
            InitializeComponent();
            _filesHandler = new FilesHandler();
        }

        /// <summary>
        /// Подключение к серверу
        /// </summary>
        private void ConnectToServer() 
        {
            _client = new TcpClient(LOCALHOST, DEFAULT_PORT);
            var t = new Thread(ProcessClientTransactions);
            t.IsBackground = true;
            t.Start(_client);
        }

        /// <summary>
        /// Отправка сообщений серверу
        /// </summary>
        /// <param name="path"></param>
        private void SendCommandButtonHandler(string path) 
        {
            var onlytext = new List<string>();
            var rez = _filesHandler.CheckFiles(path); //проверка на кол-во файлов в папке
            if (rez == true)
            {
                ConnectToServer();
                var writer = new StreamWriter(_client.GetStream());
                onlytext = _filesHandler.ReadFiles(path); 
                foreach (var value in onlytext)
                {
                    writer.WriteLine(value);
                    writer.Flush();
                }
            }
            else MessageBox.Show("В папке больше 127 файлов! Для задания необходимо меньше 127!");
        }

        /// <summary>
        /// Обработка сообщений TCP клиента
        /// </summary>
        /// <param name="tcpClient"></param>
        private void ProcessClientTransactions(object tcpClient) 
        {
            var client = (TcpClient)tcpClient;
            try
            {
                var reader = new StreamReader(client.GetStream());
                while (client.Connected)
                {
                    var input = reader.ReadLine();
                    WriteLine(" Получено от сервера: " + input);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Проблема с подключением к серверу!");
                WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// Вывод сообщений на форму
        /// </summary>
        /// <param name="text"></param>
        private void WriteLine(string text)
        {
            ServerAnswerBox.InvokeEx(stb => stb.Text += CRLF + text);
        }

        private void ClientSendButton_Click(object sender, EventArgs e)
        {
            SendCommandButtonHandler(ClientCommandBox.Text);
            ClientCommandBox.Text = string.Empty;
        }

        /// <summary>
        /// Создание файлов для проверки кол-ва (>127)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) 
        {
            _filesHandler.CreateFiles(Int32.Parse(textBox1.Text));
        }

        private void ClientCommandBox_TextChanged(object sender, EventArgs e)
        {
            ClientSendButton.Enabled = true;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _filesHandler.SaveLog(ServerAnswerBox.Text);
        }
    }
}

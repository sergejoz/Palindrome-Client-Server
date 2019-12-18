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
            var _localhost = ServerIPBox.Text;
            var _port = Int32.Parse(ServerPortBox.Text);

            _client = new TcpClient(_localhost, _port);
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
            var IsCountCheckPassed = _filesHandler.CheckFilesCount(path); //проверка на кол-во файлов в папке

            if (!IsCountCheckPassed)
            {
                MessageBox.Show("В папке больше 127 файлов! Для задания необходимо меньше 127!");
                return;
            }

            ConnectToServer();

            var writer = new StreamWriter(_client.GetStream());
            onlytext = _filesHandler.ReadFiles(path);
            foreach (var value in onlytext)
            {
                writer.WriteLine(value);
                writer.Flush();
            }

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
            ServerAnswerBox.InvokeEx(stb => stb.Text += "\r\n" + text);
        }

        private void ClientSendButton_Click(object sender, EventArgs e)
        {
            SendCommandButtonHandler(ClientCommandBox.Text);
            ClientCommandBox.Text = string.Empty;
        }

        private void ClientCommandBox_TextChanged(object sender, EventArgs e)
        {
            ClientSendButton.Enabled = true;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _filesHandler.SaveLog(ServerAnswerBox.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

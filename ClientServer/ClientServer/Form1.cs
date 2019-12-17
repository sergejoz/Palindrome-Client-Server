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
        private const string CRLF = "\r\n";
        //private const string LOCALHOST = "127.0.0.1";
        private const string LOCALHOST = "192.168.1.71"; //проверял на работе в домашней сети между компьютерами
        private const int DEFAULT_PORT = 5959;

        FilesHandler fh = new FilesHandler();
        private TcpClient _client;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void ConnectToServer() //подключение к серверу
        {
            _client = new TcpClient(LOCALHOST, DEFAULT_PORT);
            Thread t = new Thread(ProcessClientTransactions);
            t.IsBackground = true;
            t.Start(_client);
        }

        private void SendCommandButtonHandler(string path) //отправка серверу
        {
            List<(string, string)> alltext;
            var onlytext = new List<string>();
            bool rez = fh.CheckFiles(path); //проверка на кол-во файлов в папке
            if (rez == true)
            {
                ConnectToServer();
                StreamWriter writer = new StreamWriter(_client.GetStream());
                alltext = fh.ReadFiles(path); //считываем файлы в формат [название, текст]
                onlytext = fh.ListToOnlyText(alltext); //сохраняем только текст для отправки

                foreach (string value in onlytext)
                {
                    writer.WriteLine(value);
                    writer.Flush();
                }
            }
            else MessageBox.Show("В папке больше 127 файлов! Для задания необходимо меньше 127!");
        }

        private void ProcessClientTransactions(object tcpClient)  //поток для принятия ответа
        {
            TcpClient client = (TcpClient)tcpClient;
            string input = string.Empty;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());

                while (client.Connected)
                {
                    input = reader.ReadLine();


                    ServerAnswerBox.InvokeEx(stb => stb.Text += CRLF + " Получено от сервера: " + input);

                }
            }
            catch (Exception ex)
            {
                ServerAnswerBox.InvokeEx(stb => stb.Text += CRLF + "Проблема с подключением к серверу!");
            }

        }

        private void ClientSendButton_Click(object sender, EventArgs e)
        {
            SendCommandButtonHandler(ClientCommandBox.Text);
            ClientCommandBox.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e) //создавал файлы для проверки кол-ва > 127
        {
            fh.CreateFiles(Int32.Parse(textBox1.Text));
        }

        private void ClientCommandBox_TextChanged(object sender, EventArgs e)
        {
            ClientSendButton.Enabled = true;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fh.SaveLog(ServerAnswerBox.Text);
        }
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Server.Utils;

namespace Server
{
    public partial class ServerForm : Form
    {
        // перенос
        private const string CRLF = "\r\n";

        // вынес переменные
        private TcpListener _listener;
        private int _threads_count=0;
        private bool _keep_going;
        private const int DEFAULT_PORT = 5959;


        public ServerForm()
        {
            InitializeComponent();
            StartServer(); //включаем сервер при запуске
            textBox1.Text = _threads_count.ToString();
        }

        private void StartServer() //включение сервера
        {
            _threads_count = 0;
            Thread t = new Thread(ListenForIncomingConnections);
            t.Name = "Server Listener Thread";
            t.IsBackground = true;
            t.Start();
        }

        private void ListenForIncomingConnections() //прослушивание клиентов
        {
            try
            {
                _keep_going = true;
                _listener = new TcpListener(IPAddress.Any, DEFAULT_PORT);
                _listener.Start();
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "Сервер включен. Слушаю порт: " + DEFAULT_PORT);

                while (_keep_going)
                {
                    DoTask();
                }
            }
            catch (Exception ex)
            {
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "Проблемы с запуском сервера.");
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + ex.ToString());
            }
        }

        private void DoTask() //действие 
        {
            int needed = Int32.Parse(textBox2.Text);
            TcpClient client = _listener.AcceptTcpClient(); // одобряем клиента

            if (_threads_count < needed)
            {
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "Новый запрос! Начинаю выполнение...");
                Thread t = new Thread(ProcessClientRequests);
                t.IsBackground = true;
                t.Start(client);

                
            }
            else
            {
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "Новый запрос, но нет свободного потока!");
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.WriteLine("Нет свободного потока!");
                writer.Flush();
            }
        }

        private void ProcessClientRequests(object o) //потоковое задание
        {
            TcpClient client = (TcpClient)o;
            _threads_count++;
            textBox1.InvokeEx(cctb => cctb.Text = _threads_count.ToString());

            //Так как вычисление палиндрома задача для Сервера творческая, 
            //    то он никак не может затратить на нее меньше 1 секунды 
            //    (допустимо использование методов Thread.Sleep или Task.Delay)
            Thread.Sleep(3000);

            string input = string.Empty;

            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                while (client.Connected)
                {
                    input = reader.ReadLine();
                    ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "От клиента: " + input);
                    writer.WriteLine(input + ", " + Palindrom((input)));
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                ServerStatusBox.InvokeEx(stb => stb.Text += CRLF + "Проблема с подключением. ");
            }
        }

        public static string Palindrom(string s) //определение палиндрома
        {
            for (int i = 0; i < s.Length / 2; i++)

                if (s[i] != s[s.Length - i - 1])
                    return "false";
            return "true";
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) //проверка кол-ва потоков на форме
        {
            bool check;
            int maxthread = Int32.Parse(textBox2.Text);
            if ((maxthread > 0) && (maxthread <= 10))
                check = true;
            else check = false;
            if (check == false)
            {
                MessageBox.Show("Значение должно быть от 1 до 10");
                textBox2.Clear();
            }
        }
    }
}

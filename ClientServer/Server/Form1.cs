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
        // вынес переменные
        private TcpListener _listener;
        private int _threads_count = 0;
        private bool _keep_going;
        private const int DEFAULT_PORT = 5959;

        public ServerForm()
        {
            InitializeComponent();
            StartServer(); //включаем сервер при запуске
            textBox1.Text = _threads_count.ToString();
        }

        /// <summary>
        /// Включение сервера
        /// </summary>
        private void StartServer()
        {
            _threads_count = 0;
            Thread t = new Thread(ListenForIncomingConnections);
            t.Name = "Server Listener Thread";
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        ///  Прослушивание подключений клиентов
        /// </summary>
        private void ListenForIncomingConnections()
        {
            try
            {
                _keep_going = true;
                _listener = new TcpListener(IPAddress.Any, DEFAULT_PORT);
                _listener.Start();
                WriteLine("Сервер включен. Слушаю порт: " + DEFAULT_PORT);
                while (_keep_going)
                {
                    DoTask();
                }
            }
            catch (Exception ex)
            {
                WriteLine("Проблемы с запуском сервера.");
                WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Одобрение клиента и старт потока
        /// </summary>
        private void DoTask()
        {
            var needed = Int32.Parse(textBox2.Text);
            var client = _listener.AcceptTcpClient();

            if (_threads_count < needed)
            {
                WriteLine("Новый запрос! Начинаю выполнение...");
                var t = new Thread(ProcessClientRequests);
                t.IsBackground = true;
                t.Start(client);
            }
            else
            {
                WriteLine("Новый запрос, но нет свободного потока!");
                var writer = new StreamWriter(client.GetStream());
                writer.WriteLine("Нет свободного потока!");
                writer.Flush();
            }
        }

        /// <summary>
        /// Одобрение клиента и старт потока
        /// </summary>
        private void DoTaskNew()
        {
            var needed = Int32.Parse(textBox2.Text);
            var client = _listener.AcceptTcpClient();

            if (_threads_count < needed)
            {
                WriteLine("Новый запрос! Начинаю выполнение...");
                
                var t = new Thread(ProcessClientRequests);
                t.IsBackground = true;
                t.Start(client);
            }
            else
            {
                WriteLine("Новый запрос, но нет свободного потока!");
                var writer = new StreamWriter(client.GetStream());
                writer.WriteLine("Нет свободного потока!");
                writer.Flush();
            }
        }

        /// <summary>
        /// Потоковое задание
        /// </summary>
        /// <param name="tcpClient"></param>
        private void ProcessClientRequests(object tcpClient)
        {
            var client = (TcpClient)tcpClient;
            _threads_count++;
            textBox1.InvokeEx(cctb => cctb.Text = _threads_count.ToString());

            //Так как вычисление палиндрома задача для Сервера творческая, 
            //    то он никак не может затратить на нее меньше 1 секунды 
            //    (допустимо использование методов Thread.Sleep или Task.Delay)
            Thread.Sleep(3000);
            try
            {
                var reader = new StreamReader(client.GetStream());
                var writer = new StreamWriter(client.GetStream());
                while (client.Connected)
                {
                    var input = reader.ReadLine();
                    WriteLine("От клиента: " + input);
                    writer.WriteLine(input + ", " + Palindrom((input)));
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                WriteLine("Проблема с подключением.");
                _threads_count--;
                textBox1.InvokeEx(cctb => cctb.Text = _threads_count.ToString());
            }
        }

        private void ProcessClientRequestsNew(object tcpClient)
        {
            var client = (TcpClient)tcpClient;
            _threads_count++;
            textBox1.InvokeEx(cctb => cctb.Text = _threads_count.ToString());

            //Так как вычисление палиндрома задача для Сервера творческая, 
            //    то он никак не может затратить на нее меньше 1 секунды 
            //    (допустимо использование методов Thread.Sleep или Task.Delay)
            Thread.Sleep(3000);
            try
            {
                var reader = new StreamReader(client.GetStream());
                var writer = new StreamWriter(client.GetStream());
                while (client.Connected)
                {
                    var input = reader.ReadLine();
                    WriteLine("От клиента: " + input);
                    writer.WriteLine(input + ", " + Palindrom((input)));
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                WriteLine("Проблема с подключением.");
                _threads_count--;
                textBox1.InvokeEx(cctb => cctb.Text = _threads_count.ToString());
            }
        }

        /// <summary>
        /// Вывод сообщений на форму
        /// </summary>
        /// <param name="text"></param>
        private void WriteLine(string text)
        {
            ServerStatusBox.InvokeEx(stb => stb.Text += "\r\n" + text);
        }

        /// <summary>
        /// Определение палиндрома
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Palindrom(string s)
        {
            int i = 0, j = s.Length - 1;
            while (i < j)
            {
                if (char.IsWhiteSpace(s[i]) || char.IsPunctuation(s[i]))
                    i++;
                else if (char.IsWhiteSpace(s[j]) || char.IsPunctuation(s[j]))
                    j--;
                else if (char.ToLowerInvariant(s[i++]) != char.ToLowerInvariant(s[j--]))
                    return "false";
            }
            return "true";

        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Проверка количества потоков на форме 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool check;
            var maxthread = Int32.Parse(textBox2.Text);
            if ((maxthread > 0) && (maxthread <= 10))
                check = true;
            else check = false;
            if (check == false)
            {
                MessageBox.Show("Значение должно быть от 1 до 10");
                textBox2.Clear();
            }
        }

        public class UsingData
        {
            public TcpClient x { get; set; }
            public string y { get; set; }

            public UsingData(TcpClient _x, string _y)
            {
                this.x = _x;
                this.y = _y;
            }
        }
    }
}

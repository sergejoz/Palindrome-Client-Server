using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ClientServer
{
    class FilesHandler
    {
        public bool CheckFiles(string path) //проверка кол-ва
        {
            int b = Directory.GetFiles(path).Length;
            if (b > 128) return false;
            else return true;
        }


        public List<(string, string)> ReadFiles(string path) //считываем в лист с [название, текст файла]
        {
            var fileStorage = new List<(string, string)>();
            var filePaths = Directory.GetFiles(path, "*.txt"); //string[]
            foreach (string pathz in filePaths)
            {
                using (FileStream fstream = File.OpenRead(pathz))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    string textFromFile = Encoding.Default.GetString(array);
                    fileStorage.Add((Path.GetFileName(pathz), textFromFile));
                }
            }
            return fileStorage;
        }

        public List<string> ListToOnlyText(List<(string, string)> textlist) //преобразуем лист в только текст файла для отправки
        {
            var onlytext = new List<string>();
            for (int i = 0; i < textlist.Count; i++)
            {
                onlytext.Add(textlist[i].Item2);
            }
            return onlytext;
        }

        public void CreateFiles(int a) //проверка кол-ва
        {
            string pathname = "";
            string path = @"D:\STUDY\Shared\Projects\ClientServer\Data\Data1\";
            for (int i = 0; i <= a; i++)
            {
                pathname = path + (i + 1) + ".txt";
                using (FileStream fs = File.Create(pathname))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("MACROSCOP");
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        public void SaveLog(string logtext) //сохранение лога о работе приложения
        {
            string path = $"log.txt";
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(logtext);
                fs.Write(info, 0, info.Length);
            }
        }

    }
}

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ClientServer
{
    class FilesHandler
    {
        /// <summary>
        /// Проверка количества файлов в директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckFiles(string path) 
        {
            var filesCount = Directory.GetFiles(path).Length;
            return filesCount <= 127;
        }

        /// <summary>
        /// Считывание содержимвого файлов директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<(string, string)> ReadFiles(string path) 
        {
            var fileStorage = new List<(string, string)>();
            var filePaths = Directory.GetFiles(path, "*.txt"); //string[]
            foreach (var pathz in filePaths)
            {
                using (var fstream = File.OpenRead(pathz))
                {
                    var array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    var textFromFile = Encoding.Default.GetString(array);
                    fileStorage.Add((Path.GetFileName(pathz), textFromFile));
                }
            }
            return fileStorage;
        }

        /// <summary>
        /// Преобразуем лист в только текст файла для отправки
        /// </summary>
        /// <param name="textlist"></param>
        /// <returns></returns>
        public List<string> ListToOnlyText(List<(string, string)> textlist) 
        {
            var onlytext = new List<string>();
            for (var i = 0; i < textlist.Count; i++)
            {
                onlytext.Add(textlist[i].Item2);
            }
            return onlytext;
        }

        /// <summary>
        /// Создание определенного количества файлов (для проверки ограничения кол-ва)
        /// </summary>
        /// <param name="a"></param>
        public void CreateFiles(int a)
        {
            var pathname = "";
            var path = @"D:\STUDY\Shared\Projects\ClientServer\Data\Data1\";
            for (var i = 0; i <= a; i++)
            {
                pathname = path + (i + 1) + ".txt";
                using (FileStream fs = File.Create(pathname))
                {
                    var info = new UTF8Encoding(true).GetBytes("MACROSCOP");
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        /// <summary>
        /// Сохранение лога о работе приложения при закрытии 
        /// </summary>
        /// <param name="logtext"></param>
        public void SaveLog(string logtext) 
        {
            var path = $"log.txt";
            using (FileStream fs = File.Create(path))
            {
                var info = new UTF8Encoding(true).GetBytes(logtext);
                fs.Write(info, 0, info.Length);
            }
        }

    }
}

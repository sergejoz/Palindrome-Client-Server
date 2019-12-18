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
        public bool CheckFilesCount(string path)
        {
            var filesCount = Directory.GetFiles(path).Length;
            return filesCount <= 127;
        }

        /// <summary>
        /// Считывание содержимвого файлов директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> ReadFiles(string path)
        {
            var fileStorage = new List<string>();
            var filePaths = Directory.GetFiles(path, "*.txt"); //string[]
            foreach (var pathz in filePaths)
            {
                using (var fstream = File.OpenRead(pathz))
                {
                    var array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    var textFromFile = Encoding.Default.GetString(array);
                    fileStorage.Add(textFromFile);
                }
            }
            return fileStorage;
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

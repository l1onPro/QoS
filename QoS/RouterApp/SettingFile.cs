using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoS.RouterApp
{
    class SettingFile
    {
        /// <summary>
        /// Указывает путь к папке "Мои документы"
        /// </summary>
        public readonly static String pathToDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Название директории проекта
        /// </summary>
        public readonly static String nameDirectory = @"QoS info";

        /// <summary>
        /// Полный путь к проекту
        /// </summary>
        public readonly static String pathToProject = pathToDoc + "\\" + nameDirectory;

        //Пути до файлов
        public readonly static String pathForGenerationPackage = pathToProject + "\\" + @"GenerationPackage.txt";
        public readonly static String pathForQueuerings = pathToProject + "\\" + @"Queuering.txt";
        public readonly static String pathForResultQueue = pathToProject + "\\" + @"ResultQueue.txt";

        public readonly static String space = "------------------------";

        public SettingFile()
        {
            DeleteDirectory();
            CreateDirectory();
        }

        public static void PrintToFileQueuering(Queuering queue)
        {
            String text = queue.ID + ":";
            PrintToFile(pathForQueuerings, text);

            if (queue.NOTNULL())
            {
                PrintToFile(pathForQueuerings, queue.PrintToFile());
            }
        }

        public static void PrintToFileListQueuering(List<Queuering> listQueue)
        {            
            foreach (Queuering queue in listQueue)
            {
                PrintToFileQueuering(queue);                
            }
            String text = space + Environment.NewLine;
            PrintToFile(pathForQueuerings, space);
        }

        public static void PrintToFile(String path, String text)
        {
            File.AppendAllText(path, text + Environment.NewLine);
        }

        /// <summary>
        /// Удалить директорию проекта, если она есть
        /// </summary>
        public void DeleteDirectory()
        {        
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(pathToProject);
                dirInfo.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Создать директорию проекта
        /// </summary>
        public void CreateDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathToDoc);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(nameDirectory);
        }
    }
}

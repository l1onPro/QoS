using QoS.AppPackage;
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

        //названия файлов 
        public readonly static String nameForGenerationPackage = @"GenerationPackage.txt";
        public readonly static String nameForQueuerings = @"Queuering.txt";
        public readonly static String nameForResultQueue = @"ResultQueue.txt";
        public readonly static String nameExm1 = @"1.txt";
        public readonly static String nameExm2 = @"2.txt";
        public readonly static String nameExm3 = @"3.txt";

        //Пути до файлов
        public readonly static String pathForGenerationPackage = pathToProject + "\\" + nameForGenerationPackage;
        public readonly static String pathForQueuerings = pathToProject + "\\" + nameForQueuerings;
        public readonly static String pathForResultQueue = pathToProject + "\\" + nameForResultQueue;

        public readonly static String space = "------------------------";

        public SettingFile()
        {           
            CreateDirectory();
            Delete();
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
        /// Удалить некоторые файлы проекта
        /// </summary>
        public void Delete()
        {
            FileInfo fileInf = new FileInfo(pathForGenerationPackage);
            if (fileInf.Exists) { fileInf.Delete(); }

            fileInf = new FileInfo(pathForQueuerings);
            if (fileInf.Exists) { fileInf.Delete(); }

            fileInf = new FileInfo(pathForResultQueue);
            if (fileInf.Exists) { fileInf.Delete(); }
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

        /// <summary>
        /// получить значения из файла для примера
        /// </summary>
        /// <param name="num">е</param>
        public static List<PackageTest> GetExample(int num)
        {
            String path = pathToProject + "\\";
            if (num == 0) path += nameExm1;
            else if (num == 1) path += nameExm2;
            else if (num == 2) path += nameExm3;

            List<PackageTest> list = new List<PackageTest>();

            try
            {                
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    String str;
                    while ((str = sr.ReadLine()) != null)
                    {                        
                        String[] numbers = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int dSCP, length, delay;
                        
                        try
                        {
                            dSCP = Convert.ToInt32(numbers[0]);
                            length = Convert.ToInt32(numbers[1]);
                            delay = Convert.ToInt32(numbers[2]);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Не удалось конвертировать");
                        }

                        list.Add(new PackageTest(dSCP, length, delay));
                    }                    
                }

                return list;
            }
            catch (Exception e)
            {
                throw new Exception();
            }            
        }
    }
}

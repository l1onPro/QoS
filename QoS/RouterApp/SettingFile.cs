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
        public SettingFile()
        {
            DeleteDirectory();
            CreateDirectory();
        }

        public void DeleteDirectory()
        {
            string dirName = Setting.Path + "\\" + Setting.Directory;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                dirInfo.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CreateDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Setting.Path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(Setting.Directory);
        }
    }
}

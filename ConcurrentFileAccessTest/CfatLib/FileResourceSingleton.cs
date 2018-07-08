using System;
using System.Configuration;
using System.IO;

namespace CfatLib
{
    public sealed class FileResourceSingleton
    {
        private string _fullFilePath;

        private static volatile FileResourceSingleton _instance;
        private static readonly object SyncRoot = new object();

        private FileResourceSingleton()
        {
            CreateSharedFileResource();
        }

        public static FileResourceSingleton Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new FileResourceSingleton();
                }

                return _instance;
            }
        }

        private void CreateSharedFileResource()
        {
            var c = ConfigurationManager.OpenExeConfiguration("CfatLib.dll");

            var strFullFilePath = c.AppSettings.Settings["TargetResource"].Value;

            var path = Path.GetDirectoryName(strFullFilePath);

            if(string.IsNullOrWhiteSpace(path)) throw new Exception($"Invalid path: \"{path}\"");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            _fullFilePath = strFullFilePath;
        }

        public void WriteToFile(string text)
        {
            lock (SyncRoot)
            {
                using (var sw = new StreamWriter(_fullFilePath, true))
                {
                    Console.WriteLine(text);

                    sw.WriteLine(text);
                }
            }
        }
    }
}

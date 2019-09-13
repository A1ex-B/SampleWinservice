using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly ServiceConfig _config;

        public ConfigLoader(string filename)
        {
            _config = Load(filename);
        }

        private ServiceConfig Load(string filename)
        {
            //В будущем можно будет сделать загрузку из файла.
            if (filename != null)
            {
                ; //
            }

            return new ServiceConfig
            {
                ServiceDescription = "_FolderMonitor - test task.",
                ServiceDisplayName = "_FolderMonitor",
                ServiceName = "_FolderMonitor",
                //+ Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Input")
                InputFolder = "Input", 
                CompleteFolder = "",
                GarbageFolder = "",
                FileExtension = ".txt"
            };
        }

        public ServiceConfig Load()
        {
            return _config ?? throw new InvalidOperationException("Ошибка в получении конфига из класса " + nameof(ConfigLoader));
        }
    }
}

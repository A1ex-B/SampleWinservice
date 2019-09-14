using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            var config = new ServiceConfig();
            if (filename != null)
            {
                using (var reader = new StreamReader(filename))
                {
                    config = JsonConvert.DeserializeObject<ServiceConfig>(reader.ReadToEnd());
                }
            }
            else
            {
                config = new ServiceConfig
                {
                    ServiceDescription = "_FolderMonitor - test task.",
                    ServiceDisplayName = "_FolderMonitor",
                    ServiceName = "_FolderMonitor",
                    InputFolder = "Input",
                    CompleteFolder = "Complete",
                    GarbageFolder = "Garbage",
                    FileExtension = ".txt",
                    AttempsToAccessFilesystem = 5,
                    DelayForAnotherAttempt = 20,
                    LogFileFullName = @"d:\Programs\CS_progs\___TestTasks\Manzana\winservicelog.txt"
                };
            };
            return config;
        }

        public ServiceConfig Load()
        {
            return _config ?? throw new InvalidOperationException("Ошибка в получении конфига из класса " + nameof(ConfigLoader));
        }
    }
}

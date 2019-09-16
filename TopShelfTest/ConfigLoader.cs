using System;
using System.Collections.Generic;
using System.Configuration;
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

        public ConfigLoader()
        {
            _config = InitConfig();
        }

        private ServiceConfig InitConfig()
        {
            ServiceConfig config;
            try
            {
                config = new ServiceConfig
                {
                    ServiceDescription = ConfigurationManager.AppSettings.Get("ServiceDescription"),
                    ServiceDisplayName = ConfigurationManager.AppSettings.Get("ServiceDisplayName"),
                    ServiceName = ConfigurationManager.AppSettings.Get("ServiceName"),
                    InputFolder = ConfigurationManager.AppSettings.Get("InputFolder"),
                    CompleteFolder = ConfigurationManager.AppSettings.Get("CompleteFolder"),
                    GarbageFolder = ConfigurationManager.AppSettings.Get("GarbageFolder"),
                    FileExtension = ConfigurationManager.AppSettings.Get("FileExtension"),
                    AttempsToAccessFilesystem = Int32.Parse(ConfigurationManager.AppSettings.Get("AttempsToAccessFilesystem")),
                    DelayForAnotherAttempt = Int32.Parse(ConfigurationManager.AppSettings.Get("DelayForAnotherAttempt")),
                    LogFileFullName = ConfigurationManager.AppSettings.Get("LogFileFullName")
                };
            }
            catch (Exception ex) //default
            {
                Console.WriteLine($"Error in {nameof(InitConfig)}:\n{ex.GetType()}:\n{ex.Message}");
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
            }
            return config;
        }

        public ServiceConfig Load()
        {
            return _config ?? throw new InvalidOperationException("Ошибка в получении конфига из класса " + nameof(ConfigLoader));
        }
    }
}

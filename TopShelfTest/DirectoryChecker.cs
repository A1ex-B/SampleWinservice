using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DirectoryChecker : IDirectoryChecker
    {
        private readonly ServiceConfig _config;

        public DirectoryChecker(IConfigLoader configLoader)
        {
            if (configLoader == null)
            {
                throw new ArgumentNullException(nameof(configLoader));
            }
            _config = configLoader.Load();
        }

        public void CheckDirectories()
        {
            foreach (var path in new string[]
            {
                _config.InputFolder,
                _config.GarbageFolder,
                _config.CompleteFolder
            })
            {
                if (!Directory.Exists(path))
                {
                    var info = Directory.CreateDirectory(path);
                }
            }
        }
    }
}

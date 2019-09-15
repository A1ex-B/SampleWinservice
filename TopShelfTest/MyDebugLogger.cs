using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Service
{
    public class MyDebugLogger : ILogger
    // Для дебага, не по ТЗ.
    {
        private readonly object _syncObject;
        private readonly ServiceConfig _config;
        public MyDebugLogger(IConfigLoader loader)
        {
            _syncObject = new object();
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }
            _config = loader.Load();
        }
        public void Log(string message)
        {
            lock (_syncObject)
            {
                try
                {
                    using (var w = new StreamWriter(_config.LogFileFullName, true))
                    {
                        w.WriteLine(message);
                    }
                }
                catch(Exception)
                {
                    ;// Это для дебага. Вообще так нельзя, но я давлю все исключения в своём логгере.
                }
            }
        }
    }
}

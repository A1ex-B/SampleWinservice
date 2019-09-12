﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace TopShelfTest
{
    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += async (sender, eventArgs) =>
            {
                Console.WriteLine("It is {0} and all is well", DateTime.Now);
                using (var service = new ServiceClient())
                {
                    await service.GetReceiptsAsync(11);
                }
            };
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }

    public class Program
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>                                   //1
            {
                x.Service<TownCrier>(s =>                                   //2
                {
                    s.ConstructUsing(name => new TownCrier());                //3
                    s.WhenStarted(tc => tc.Start());                         //4
                    s.WhenStopped(tc => tc.Stop());
                    
                    var watcher = new FileSystemWatcher();//5
                });
                
                x.RunAsLocalSystem();                                       //6
                
                x.SetDescription("Aaaaaaaa Descrition___Sample Topshelf Host");                   //7
                x.SetDisplayName("AaaaaaDisplayName!");                                  //8
                x.SetServiceName("AaaaaaaaServiceName");                                  //9
            });                                                             //10

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Environment.ExitCode = exitCode;
        }
    }
}
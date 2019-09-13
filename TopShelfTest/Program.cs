using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Topshelf;

namespace TopShelfTest
{
    public class Program
    {
        public static void Main()
        {
            //XmlConfigurator.Configure();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DIModule());
            using (var container = builder.Build())
            {
                var rc = HostFactory.Run(hostConfigurator =>
                {
                    hostConfigurator.SetServiceName("MyService");
                    hostConfigurator.SetDisplayName("My Service");
                    hostConfigurator.SetDescription("Does custom logic.");

                    hostConfigurator.RunAsLocalSystem();
                //hostConfigurator.UseLog4Net();

                hostConfigurator.Service<ITownCrier>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(hostSettings => container.Resolve<ITownCrier>());
                        serviceConfigurator.WhenStarted(service => service.Start());
                        serviceConfigurator.WhenStopped(service => service.Stop());
                    });
                });
                var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
                Environment.ExitCode = exitCode;
            }
        }
    }
}

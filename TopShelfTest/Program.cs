using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Topshelf;

namespace Service
{
    public class Program
    {
        public static void Main()
        {
            //XmlConfigurator.Configure();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DIModule("SomePath\\SomeFilename.cfg"));
            using (var container = builder.Build())
            {
                var launcher = container.Resolve<IServiceLauncher>();
                var exitCode = launcher.Launch();
                Environment.ExitCode = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            }
        }
    }
}

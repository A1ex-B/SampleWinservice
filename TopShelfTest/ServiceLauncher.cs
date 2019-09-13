using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Service
{
    public class ServiceLauncher : IServiceLauncher
    {
        private readonly IService _service;
        private readonly ServiceConfig _config;

        public ServiceLauncher(IService service, IConfigLoader configLoader)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            if (configLoader == null)
            {
                throw new ArgumentNullException(nameof(configLoader));
            }
            _config = configLoader.Load();
        }

        public TopshelfExitCode Launch()
        {
            var rc = HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.SetServiceName(_config.ServiceName);
                hostConfigurator.SetDisplayName(_config.ServiceDisplayName);
                hostConfigurator.SetDescription(_config.ServiceDescription);

                hostConfigurator.RunAsLocalSystem();
                //hostConfigurator.UseLog4Net();

                hostConfigurator.Service<IService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(hostSettings => _service);
                    serviceConfigurator.WhenStarted(service => service.Start());
                    serviceConfigurator.WhenStopped(service => service.Stop());
                });
            });
            return rc;
        }
    }
}

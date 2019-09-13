using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Service
{
    class DIModule : Module
    {
        private readonly string _fileName;
        public DIModule(string configFileName)
        {
            _fileName = configFileName ?? throw new ArgumentNullException(nameof(configFileName));
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FolderMonitor>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            builder.RegisterType<ServiceLauncher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ConfigLoader>().WithParameter(new NamedParameter("filename", _fileName)).AsImplementedInterfaces()
                            .SingleInstance();
            builder.RegisterType<FileProcessor>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<FileReader>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}

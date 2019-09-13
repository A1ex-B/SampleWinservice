using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TopShelfTest
{
    class DIModule : Module
    {
        public DIModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TownCrier>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
        }
    }
}

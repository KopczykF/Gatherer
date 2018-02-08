using System.Reflection;
using Autofac;
using Gatherer.Infrastructure.Commands;

namespace Gatherer.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            // builder.RegisterType<RegisterHandler>()
            //     .As<ICommandDispatcher<Register>>()
            //     .InstancePerLifetimeScope();


            
            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
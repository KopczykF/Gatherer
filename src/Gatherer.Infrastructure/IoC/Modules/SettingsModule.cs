using Autofac;
using Gatherer.Infrastructure.Extensions;
using Gatherer.Infrastructure.Mongo;
using Gatherer.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Gatherer.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
                .SingleInstance();
        }
    }
}
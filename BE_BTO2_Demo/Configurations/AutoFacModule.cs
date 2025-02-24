using Autofac;
using AutoMapper;
using System.Reflection;

namespace BE_BTO2_Demo.Configurations
{
    public class AutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Dang ky AutoMapper
            builder.Register(c => {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var config = new MapperConfiguration(mcf =>
                {
                    mcf.AddMaps(assemblies);
                });
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();

            // Tu dong dang ky Repository co Interface
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Tu dong dang ky Service co Interface
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}

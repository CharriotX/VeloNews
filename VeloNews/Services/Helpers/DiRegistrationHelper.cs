using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.Linq;
using System.Reflection;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services.Helpers
{
    public class DiRegistrationHelper
    {
        public void RegisterAllRepositories(IServiceCollection services)
        {
            var dataInterfaceAssebly = Assembly.GetAssembly(typeof(IBaseRepository<>));
            var repositoriesAssembly = Assembly.GetAssembly(typeof(BaseRepository<>));

            dataInterfaceAssebly
                .GetTypes()
                .Where(
                    t => t.IsInterface
                    && t
                        .GetInterfaces()
                        .Any(i =>
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)
                       )
                   )
                .ToList()
                .ForEach(repositoryInterface =>
                {
                    services.AddScoped(repositoryInterface, serviceProvider =>
                    {
                        var contructorRepository =
                            repositoriesAssembly
                                .GetTypes()
                                .First(classType =>
                                    classType.IsClass
                                    && classType.GetInterfaces().Any(i => i == repositoryInterface))
                                .GetConstructors()
                                .OrderByDescending(x => x.GetParameters().Length)
                                .First();

                        var repositoryObj = contructorRepository
                            .Invoke(contructorRepository
                                .GetParameters()
                                .Select(param => serviceProvider.GetService(param.ParameterType))
                                .ToArray());

                        return repositoryObj;
                    });
                });
        }

        public void RegisterAllServices(IServiceCollection services)
        {
            var type = typeof(AutoDiServiceRegistrationAttribute);
            var serviceAssembly = Assembly.GetAssembly(type);
            var definedTypes = serviceAssembly.DefinedTypes;

            var myServices = definedTypes
                .Where(type => type.GetTypeInfo()
                    .GetCustomAttribute<AutoDiServiceRegistrationAttribute>() != null)
                .ToList();

            var serviceInterface = myServices.Select(x => x.ImplementedInterfaces.First()).ToList();


            serviceInterface.ForEach(serviceInterface =>
            {
                services.AddScoped(serviceInterface, serviceProvider =>
                {
                    var serviceConstructor =
                        serviceAssembly
                            .GetTypes()
                            .First(classType =>
                                classType.IsClass
                                && classType.GetInterfaces().Any(x => x == serviceInterface))
                            .GetConstructors()
                            .OrderByDescending(x => x.GetParameters().Length)
                            .First();

                    var serviceObj = serviceConstructor
                        .Invoke(serviceConstructor
                            .GetParameters()
                            .Select(param => serviceProvider.GetService(param.ParameterType))
                            .ToArray());

                    return serviceObj;
                });
            });

            var z = 6;
        }
    }
}

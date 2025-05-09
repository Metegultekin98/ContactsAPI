using System.Reflection;
using Core.Application.Base.Rules;
using Core.Application.Pipelines.CheckId;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.Helpers.Extensions;
using Core.Utilities.MessageBrokers.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace webAPI.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CheckIdBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });


        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped(typeof(BaseBusinessRules<,>));
        services.AddScoped<IMessageConsumer, MqConsumerHelper>();
        services.AddScoped<IMessageBrokerHelper, MqQueueHelper>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }

    public static IServiceCollection AddScopedWithManagers(this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
                                   .Where(t => t.IsInterface && t.Name.EndsWith("Service"));

        foreach (var serviceType in serviceTypes)
        {
            var managerTypeName = serviceType.Name.Replace("Service", "Manager").ReplaceFirst("I", "");
            var managerType = assembly.GetTypes().SingleOrDefault(t => t.Name == managerTypeName);

            if (managerType != null)
            {
                services.AddScoped(serviceType, managerType);
            }
        }

        return services;
    }
}
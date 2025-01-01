using Autofac;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.Infrastructure.Services;

namespace PaymentProcessingSystem.Extensions;

public class ApplicationModule : Module
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    public ApplicationModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Resolve the dependencies
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

        //repositories dependencies

        builder.RegisterType(typeof(PaymentContext)).As(typeof(IEntitiesContext)).InstancePerLifetimeScope();
    }
}

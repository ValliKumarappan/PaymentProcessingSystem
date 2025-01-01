using Autofac;
using MediatR.Pipeline;
using MediatR;
using System.Reflection;

namespace PaymentProcessingSystem.Extensions
{
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// Magic
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
    .AsImplementedInterfaces().InstancePerLifetimeScope();




            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();


            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));




        }
    }
}

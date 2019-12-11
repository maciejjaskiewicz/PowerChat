using Autofac;
using MediatR;
using PowerChat.Application.Common.Behaviors;

namespace PowerChat.Application.IoC.Modules
{
    internal class PipelineModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RequestExceptionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPerformanceBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestValidationBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}

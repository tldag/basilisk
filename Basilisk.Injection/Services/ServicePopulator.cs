using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Basilisk.Injection.Services
{
    /// <summary>
    /// ServicePopulator
    /// </summary>
    public class ServicePopulator
    {
        /// <summary>
        /// The builder.
        /// </summary>
        protected ContainerBuilder Builder { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="builder">The builder.</param>
        public ServicePopulator(ContainerBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Creates a new populator.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The populator.</returns>
        public static ServicePopulator Create(ContainerBuilder builder)
            => new(builder);

        /// <summary>
        /// Populates the builder with service provider, service scope factory and the givev services.
        /// </summary>
        /// <param name="descriptors"></param>
        public virtual void Populate(IEnumerable<ServiceDescriptor> descriptors)
        {
            RegisterServiceProvider();
            RegisterScopeFactory();
            RegisterServices(descriptors);
        }

        /// <summary>
        /// Registers the service provider.
        /// </summary>
        protected virtual void RegisterServiceProvider()
        {
            Builder.RegisterType<AutofacServiceProvider>()
                .As<IServiceProvider>()
                .As<ISupportRequiredService>()
                .As<IServiceProviderIsService>()
                .SingleInstance();
        }

        /// <summary>
        /// Registers the scope factory.
        /// </summary>
        protected virtual void RegisterScopeFactory()
        {
            Builder.RegisterType<ServiceScopeFactory>()
                .As<IServiceScopeFactory>()
                .SingleInstance();
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="descriptors"></param>
        protected virtual void RegisterServices(IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (ServiceDescriptor descriptor in descriptors)
            {
                RegisterService(descriptor);
            }
        }

        /// <summary>
        /// Registers the service.
        /// </summary>
        /// <param name="descriptor"></param>
        protected virtual void RegisterService(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationType is not null)
            {
                RegisterServiceType(descriptor);
            }
            else if (descriptor.ImplementationFactory is not null)
            {
                RegisterServiveFactory(descriptor);
            }
            else
            {
                RegisterServiceInstance(descriptor);
            }
        }

        /// <summary>
        /// Registers a service with <c>ImplementationType</c> is not null.
        /// </summary>
        /// <param name="descriptor"></param>
        protected virtual void RegisterServiceType(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationType is null)
                return;

            if (descriptor.ServiceType.GetTypeInfo().IsGenericTypeDefinition)
            {
                Builder.RegisterGeneric(descriptor.ImplementationType).As(descriptor.ServiceType).ConfigureLifecycle(descriptor);
            }
            else
            {
                Builder.RegisterType(descriptor.ImplementationType).As(descriptor.ServiceType).ConfigureLifecycle(descriptor);
            }
        }

        /// <summary>
        /// Registers a service with <c>ImplementationFactory</c> is not null.
        /// </summary>
        /// <param name="descriptor"></param>
        protected virtual void RegisterServiveFactory(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationFactory is null)
                return;

            object factory(IComponentContext context, IEnumerable<Parameter> _)
                => descriptor.ImplementationFactory(context.Resolve<IServiceProvider>());

            IComponentRegistration registration = RegistrationBuilder
                .ForDelegate(descriptor.ServiceType, factory)
                .ConfigureLifecycle(descriptor)
                .CreateRegistration();

            Builder.RegisterComponent(registration);
        }

        /// <summary>
        /// Registers a service with <c>ImplementationInstance</c> is not null.
        /// </summary>
        /// <param name="descriptor"></param>
        protected virtual void RegisterServiceInstance(ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance is null)
                return;

            Builder.RegisterInstance(descriptor.ImplementationInstance).As(descriptor.ServiceType).ConfigureLifecycle(descriptor);
        }
    }
}

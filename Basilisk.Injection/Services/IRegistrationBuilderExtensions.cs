using Autofac.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Injection.Services
{
    /// <summary>
    /// <c>IRegistrationBuilder</c> extensions.
    /// </summary>
    public static class IRegistrationBuilderExtensions
    {
        /// <summary>
        /// Configures the lifetime for the service component under construction.
        /// </summary>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<object, TActivatorData, TRegistrationStyle> ConfigureLifecycle<TActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<object, TActivatorData, TRegistrationStyle> registrationBuilder, ServiceDescriptor descriptor)
        => registrationBuilder.ConfigureLifecycle(descriptor.Lifetime);

        /// <summary>
        /// Configures the lifetime for the service component under construction.
        /// </summary>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="lifetime"></param>
        /// <returns><c>registrationBuilder</c></returns>
        public static IRegistrationBuilder<object, TActivatorData, TRegistrationStyle> ConfigureLifecycle<TActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<object, TActivatorData, TRegistrationStyle> registrationBuilder, ServiceLifetime lifetime)
        {
            return lifetime switch
            {
                ServiceLifetime.Singleton => registrationBuilder.SingleInstance(),
                ServiceLifetime.Scoped => registrationBuilder.InstancePerLifetimeScope(),
                ServiceLifetime.Transient => registrationBuilder.InstancePerDependency(),
                _ => registrationBuilder,
            };
        }
    }
}

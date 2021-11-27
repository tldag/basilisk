using Autofac.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Inject.Services
{
    /// <summary>
    /// <c>IRegistrationBuilder</c> extensions.
    /// </summary>
    public static class IRegistrationBuilderExtensions
    {
        /// <summary>
        /// Configures the lifetime for the service component under construction.
        /// </summary>
        /// <typeparam name="TLimit"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> ConfigureLifecycle<TLimit, TActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, ServiceDescriptor descriptor)
        => registrationBuilder.ConfigureLifecycle(descriptor.Lifetime);

        /// <summary>
        /// Configures the lifetime for the service component under construction.
        /// </summary>
        /// <typeparam name="TLimit"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="lifetime"></param>
        /// <returns><c>registrationBuilder</c></returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> ConfigureLifecycle<TLimit, TActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, ServiceLifetime lifetime)
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

using Autofac;
using Basilisk.Injection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Basilisk.Injection
{
    /// <summary>
    /// <see cref="InjectorBuilder"/> extensions.
    /// </summary>
    public static class InjectorBuilderExtensions
    {
        /// <summary>
        /// Configure the internal <see cref="ContainerBuilder"/>
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="configurer"></param>
        /// <returns></returns>
        public static IInjectorBuilder Configure(this IInjectorBuilder sb, Action<ContainerBuilder> configurer)
        { sb.AutofacConfig.Add(configurer); return sb; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static IInjectorBuilder AddSingleton<TService, TImplementor>(this IInjectorBuilder sb)
            where TService : notnull where TImplementor : class
            => sb.Configure(cb => cb.RegisterType<TImplementor>().As<TService>().SingleInstance());

        /// <summary>
        /// Registers a type with its implemented interfaces.
        /// </summary>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static IInjectorBuilder AddSingleton<TImplementor>(this IInjectorBuilder sb)
            where TImplementor : class
            => sb.Configure(cb => cb.RegisterType<TImplementor>().AsImplementedInterfaces().SingleInstance());

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sb"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IInjectorBuilder AddInstance<T>(this IInjectorBuilder sb, T instance) where T : class
            => sb.Configure(cb => cb.RegisterInstance(instance));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IInjectorBuilder AddHostedService<TImplementor>(this IInjectorBuilder sb, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementor : IHostedService
            => sb.Configure(cb => cb.RegisterType<TImplementor>().As<IHostedService>().ConfigureLifecycle(lifetime));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="contentRoot"></param>
        /// <returns></returns>
        public static IInjectorBuilder SetContentRoot(this IInjectorBuilder sb, string contentRoot)
        { sb.UseContentRoot(contentRoot); return sb; }
    }
}

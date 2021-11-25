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
        public static InjectorBuilder Configure(this InjectorBuilder sb, Action<ContainerBuilder> configurer)
        { configurer(sb.Builder); return sb; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static InjectorBuilder AddSingleton<TService, TImplementor>(this InjectorBuilder sb)
            where TService : notnull where TImplementor : class
        { sb.Builder.RegisterType<TImplementor>().As<TService>().SingleInstance(); return sb; }

        /// <summary>
        /// Registers a type with its implemented interfaces.
        /// </summary>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static InjectorBuilder AddSingleton<TImplementor>(this InjectorBuilder sb)
            where TImplementor : class
        { sb.Builder.RegisterType<TImplementor>().AsImplementedInterfaces().SingleInstance(); return sb; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sb"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static InjectorBuilder AddInstance<T>(this InjectorBuilder sb, T instance) where T : class
        { sb.Builder.RegisterInstance(instance); return sb; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementor"></typeparam>
        /// <param name="sb"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static InjectorBuilder AddHostedService<TImplementor>(this InjectorBuilder sb, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementor : IHostedService
        { sb.Builder.RegisterType<TImplementor>().As<IHostedService>().ConfigureLifecycle(lifetime); return sb; }
    }
}

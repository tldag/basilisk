using Basilisk.Injection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.SystemServices
{
    /// <summary>
    /// <see cref="ISystemService"/> host.
    /// </summary>
    public abstract class SystemServiceHost
    {
        /// <summary>
        /// Service name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// C'tor.
        /// </summary>
        /// <param name="name">Service name.</param>
        protected SystemServiceHost(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Creates a new <see cref="SystemServiceHost{T}"/> for the given service type.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="name">Service name.</param>
        /// <returns></returns>
        public static SystemServiceHost<T> Create<T>(string name) where T : class, ISystemService
            => new(name);

        /// <summary>
        /// Runs the service or handles the command line ("install" and "uninstall" by default).
        /// </summary>
        /// <param name="args"></param>
        public void Run(params string[] args)
        {
            if (HandleCommandLine(args))
                return;

            RunAsync(CancellationToken.None).Wait();
        }

        /// <summary>
        /// Runs the service in a stoppable mode (used internally and for testing).
        /// </summary>
        /// <param name="stopToken">The token to stop the service.</param>
        /// <returns>Awaitale task.</returns>
        public async Task RunAsync(CancellationToken stopToken)
        {
            using IInjector injector = CreateInjector();

            await injector.RunAsync(stopToken);
        }

        /// <summary>
        /// Creates the injector containing the service.
        /// </summary>
        /// <returns></returns>
        protected virtual IInjector CreateInjector()
        {
            InjectorBuilder builder = new();

            AddService(builder);
            // TODO: add infrastructure

            return builder.Build();
        }

        /// <summary>
        /// Registers the service into the given builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected abstract void AddService(InjectorBuilder builder);

        /// <summary>
        /// Handles "install" and "uninstall".
        /// </summary>
        /// <param name="args">command line args</param>
        /// <returns>whether the comman line has been handled and the application may terminate immediately.</returns>
        protected virtual bool HandleCommandLine(string[] args)
        {
            if (args.Length == 0)
                return false;

            if ("install".Equals(args[0]))
                return true; // TODO: Install service

            if ("uninstall".Equals(args[0]))
                return true; // TODO: Uninstall service

            return false;
        }
    }

    /// <summary>
    /// Typed version of <see cref="SystemServiceHost"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SystemServiceHost<T> : SystemServiceHost where T : class, ISystemService
    {
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="name">Service name.</param>
        public SystemServiceHost(string name) : base(name) { }

        /// <inheritdoc/>
        protected override void AddService(InjectorBuilder builder)
        { builder.AddSingleton<T>(); }
    }
}

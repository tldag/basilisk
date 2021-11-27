using System.Collections.Generic;

namespace Basilisk.Inject.Implementation
{
    public partial class InjectorBuilder : Inject.InjectorBuilder
    {
        private IDictionary<object, object>? properties = null;

        /// <inheritdoc/>
        public override IDictionary<object, object> Properties => properties ??= HostConfig.Properties;
    }
}
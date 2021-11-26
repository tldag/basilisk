using System.Collections.Generic;

namespace Basilisk.Injection.Internal
{
    public partial class InjectorBuilder : Injection.InjectorBuilder
    {
        private IDictionary<object, object>? properties = null;

        /// <inheritdoc/>
        public override IDictionary<object, object> Properties => properties ??= HostConfig.Properties;
    }
}
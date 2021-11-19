using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basilisk.Reflection
{
    /// <summary>
    /// Property detection support.
    /// </summary>
    public class PropertyFinder
    {
        private readonly Type type;

        private string? name = null;
        private Type? requiredType = null;
        private List<Type>? requiredAttributes = null;
        private bool requireRead = false;

        /// <summary>
        /// Constructs a new property finder for the given type and optional name.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="name">Optional name of the property to find.</param>
        public PropertyFinder(Type type, string? name = null)
        {
            this.type = type;
            this.name = name;
        }

        /// <summary>
        /// Creates a new property finder for the given type and optional name.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="name">Optional name of the property to find.</param>
        /// <returns></returns>
        public static PropertyFinder Create(Type type, string? name = null)
            => new(type, name);

        /// <summary>
        /// Sets the required property type.
        /// </summary>
        /// <param name="requiredType">The required property type.</param>
        /// <returns>This instance.</returns>
        public PropertyFinder RequireType(Type requiredType)
        {
            this.requiredType = requiredType;

            return this;
        }

        /// <summary>
        /// Adds a readable requirement.
        /// </summary>
        /// <returns>This instance.</returns>
        public PropertyFinder RequireRead()
        {
            requireRead = true;

            return this;
        }

        /// <summary>
        /// Adds a required attribute type.
        /// </summary>
        /// <param name="attributeType">The type to add.</param>
        /// <returns>This instance.</returns>
        public PropertyFinder RequireAttribute(Type attributeType)
        {
            (requiredAttributes ??= new()).Add(attributeType);

            return this;
        }

        /// <summary>
        /// Finds the properties for the configured search options.
        /// </summary>
        /// <returns>The properties found.</returns>
        public IEnumerable<PropertyInfo> Find()
        {
            IEnumerable<PropertyInfo> properties = GetProperties();

            if (requiredType is not null)
                properties = properties.Where(p => p.PropertyType == requiredType);

            if (requireRead)
                properties = properties.Where(p => p.CanRead);

            if (requiredAttributes is not null)
                properties = properties.Where(p => HasAllAttributes(p, requiredAttributes));

            return properties;
        }

        private static bool HasAllAttributes(PropertyInfo property, IEnumerable<Type> requiredAttributes)
        {
            List<Type> attributes = property.CustomAttributes.Select(a => a.AttributeType).ToList();

            foreach (Type requiredAttribute in requiredAttributes)
            {
                if (!attributes.Contains(requiredAttribute))
                    return false;
            }

            return true;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (name is null)
                return type.GetProperties();

            if (type.GetProperty(name) is PropertyInfo property)
                return new[] { property };

            return Array.Empty<PropertyInfo>();
        }
    }
}

using Basilisk.Core;
using Basilisk.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static Basilisk.Core.Exceptions;
using static Basilisk.IO.Resources.IOResources;

namespace Basilisk.IO
{
    /// <summary>
    /// XML Support.
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Default (pretty) XmlWriterSettings.
        /// </summary>
        public static XmlWriterSettings DefaultSettings
        {
            get => new() { Encoding = Encoding.UTF8, Indent = true, IndentChars = "  " };
        }

        /// <summary>
        /// Deserializes XML from the given file.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="file">The file to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeXml<T>(this FileInfo file)
            where T : class
        {
            Type type = typeof(T);
            XmlSerializer serializer = new(type);
            using FileStream stream = new(file.FullName, FileMode.Open);

            return serializer.Deserialize(stream) as T ?? throw BadFileFormat(BadXmlFormatFormat.Format(type), file);
        }

        /// <summary>
        /// Deserializes XML from the given file.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="xml">The XML to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeXml<T>(this string xml)
            where T : class
        {
            Type type = typeof(T);
            XmlSerializer serializer = new(type);
            using StringReader reader = new(xml);

            return serializer.Deserialize(reader) as T ?? throw BadFileFormat(BadXmlFormatFormat.Format(type), "");
        }

        /// <summary>
        /// Serializes the given object as XML to the given file.
        /// </summary>
        /// <typeparam name="T">XML serializable type.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="file">The file to create.</param>
        /// <param name="settings">Optional settings. Default settings are used, if <c>null</c>.</param>
        public static void SerializeXml<T>(this T obj, FileInfo file, XmlWriterSettings? settings = null)
            where T : class
        {
            using FileStream stream = new(file.FullName, FileMode.Create);

            SerializeXml(obj, stream, settings);
        }

        /// <summary>
        /// Serializes the given object as XML into a string.
        /// </summary>
        /// <typeparam name="T">XML serializable type.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="settings">Optional settings. Default settings are used, if <c>null</c>.</param>
        /// <returns></returns>
        public static string SerializeXml<T>(this T obj, XmlWriterSettings? settings = null)
            where T : class
        {
            using MemoryStream stream = new();

            settings = SerializeXml(obj, stream, settings);
            stream.Position = 0;

            StreamReader reader = new(stream, settings.Encoding);

            return reader.ReadToEnd();
        }

        private static XmlWriterSettings SerializeXml<T>(T obj, Stream stream, XmlWriterSettings? settings = null)
            where T : class
        {
            settings ??= DefaultSettings;
            XmlSerializerNamespaces? namespaces = GetNamespaces(obj);
            Type type = typeof(T);
            XmlSerializer serializer = new(type, GetDefaultNamespace(type, namespaces));
            using XmlWriter writer = XmlWriter.Create(stream, settings);

            serializer.Serialize(writer, obj, namespaces);

            return settings;
        }

        private static XmlSerializerNamespaces? GetNamespaces(object obj)
        {
            PropertyInfo? property = PropertyFinder.Create(obj.GetType())
                .RequireType(typeof(XmlSerializerNamespaces))
                .RequireRead()
                .RequireAttribute(typeof(XmlNamespaceDeclarationsAttribute))
                .Find().FirstOrDefault();

            if (property is not null)
                return property.GetValue(obj) as XmlSerializerNamespaces;

            // TODO: XmlNamespaceDeclarationsAttribute can be set on fields too!

            return null;
        }

        private static string? GetDefaultNamespace(Type type, XmlSerializerNamespaces? namespaces)
        {
            if (type.GetCustomAttribute<XmlRootAttribute>()?.Namespace is string ns)
                return ns;

            if (namespaces is null)
                return null;

            return namespaces.ToArray()
                .Where(n => string.IsNullOrWhiteSpace(n.Name))
                .Select(n => n.Namespace)
                .FirstOrDefault();
        }
    }
}

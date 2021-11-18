using Basilisk.Core;
using System;
using System.IO;
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
        /// Deserializes XML from the given file.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="file">The file to deserialize.</param>
        /// <returns>The deerialized object.</returns>
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
        /// <returns>The deerialized object.</returns>
        public static T DeserializeXml<T>(this string xml)
            where T : class
        {
            Type type = typeof(T);
            XmlSerializer serializer = new(type);
            using StringReader reader = new(xml);

            return serializer.Deserialize(reader) as T ?? throw BadFileFormat(BadXmlFormatFormat.Format(type), "");
        }
    }
}

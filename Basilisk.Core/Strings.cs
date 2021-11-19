using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Basilisk.Core
{
    /// <summary>
    /// String support.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Tests whether the given character is a digit.
        /// </summary>
        /// <param name="c">The character to test.</param>
        /// <returns>Whether the given character is within <c>['0'..'9']</c>.</returns>
        public static bool IsDigit(this char c)
            => c >= '0' && c <= '9';

        /// <summary>
        /// Tests whether the given characters are all digits.
        /// </summary>
        /// <param name="chars">The characters to test.</param>
        /// <returns>Whether the given characters are all within <c>['0'..'9']</c>.</returns>
        public static bool IsDigits(this IEnumerable<char> chars)
            => chars.All(c => c.IsDigit());

        /// <summary>
        /// Joins the given strings into a single string.
        /// </summary>
        /// <param name="strings">The strings to join. May be empty.</param>
        /// <param name="separator">The separator to put between the given strings.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> strings, string separator)
            => string.Join(separator, strings);

        /// <summary>
        /// Uses the given string as format for a call to <c>string.Format</c>.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this string format, params object[] args)
            => string.Format(format, args);

        /// <summary>
        /// Uses the given string and forma provider for a call to <c>string.Format</c>.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this string format, IFormatProvider formatProvider, params object[] args)
            => string.Format(format, formatProvider, args);

        private static StreamReader GetReader(this Stream stream, Encoding? encoding = null)
            => encoding is null ? new(stream) : new(stream, encoding);

        /// <summary>
        /// Reads lines from the given stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="encoding">Optional encoding of the stream.</param>
        /// <returns>One line after the other.</returns>
        public static IEnumerable<string> ReadLines(this Stream stream, Encoding? encoding = null)
        {
            using StreamReader reader = GetReader(stream, encoding);

            while (reader.ReadLine() is string line)
                yield return line;
        }

        /// <summary>
        /// Converts the given text into a sequence of lines.
        /// </summary>
        /// <param name="text">The text to split into lines.</param>
        /// <returns>One line after the other.</returns>
        public static IEnumerable<string> ToLines(this string text)
        {
            using StringReader reader = new(text);

            while (reader.ReadLine() is string line)
                yield return line;
        }

        /// <summary>
        /// Reads lines from the given file.
        /// </summary>
        /// <param name="file">The file to read from.</param>
        /// <param name="encoding">Optional encoding of the file.</param>
        /// <returns>One line after the other.</returns>
        public static IEnumerable<string> ReadLines(this FileInfo file, Encoding? encoding = null)
        {
            using FileStream stream = new(file.FullName, FileMode.Open);
            using StreamReader reader = GetReader(stream, encoding);

            while (reader.ReadLine() is string line)
                yield return line;
        }

        /// <summary>
        /// Converts the given text to a <c>XmlDocument</c>
        /// </summary>
        /// <param name="xml">the XML text</param>
        /// <returns>The XmlDocument</returns>
        public static XmlDocument ToXmlDocument(this string xml)
        {
            XmlDocument document = new();

            document.LoadXml(xml);

            return document;
        }
    }
}

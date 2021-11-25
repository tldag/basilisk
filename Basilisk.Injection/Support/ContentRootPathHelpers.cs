using System.IO;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// ContentRootPathHelpers
    /// </summary>
    public static class ContentRootPathHelpers
    {
        /// <summary>
        /// Resolves the content root path
        /// </summary>
        /// <param name="contentRootPath"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string ResolveContentRootPath(string contentRootPath, string basePath)
        {
            if (string.IsNullOrWhiteSpace(contentRootPath))
                return basePath;

            if (Path.IsPathRooted(contentRootPath))
                return contentRootPath;

            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
        }
    }
}

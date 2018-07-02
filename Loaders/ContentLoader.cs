using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Content;

namespace SGame.Loaders
{
    public class ContentLoader
    {
        private readonly ContentManager contentManager;

        public ContentLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public Dictionary<string, T> LoadContent<T>(string directory)
        {
            var content = new Dictionary<string, T>();
            var fileNames = GetNamesFromDirectory(directory);

            foreach (var fileName in fileNames)
            {
                content[fileName] = contentManager.Load<T>(fileName);
            }

            LogInfoAboutLoadedContent<T>(fileNames);

            return content;
        }

        private void LogInfoAboutLoadedContent<T>(IEnumerable<string> fileNames)
        {
            var typeName = typeof(T).Name;
            var count = fileNames.Count();
            var items = string.Join(", ", fileNames);
            System.Console.WriteLine($"Loaded<{typeName}>({count}): {{ {items} }}");
        }

        private IEnumerable<string> GetNamesFromDirectory(string subDirectory)
        {
            var appDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var contentDirectory = Path.Combine(appDirectory, contentManager.RootDirectory, subDirectory);
            var filePaths = Directory.GetFiles(contentDirectory, "*.xnb");
            var fileNames = filePaths.Select(x => Path.Combine(subDirectory, Path.GetFileNameWithoutExtension(x)));
            return fileNames;
        }
    }
}
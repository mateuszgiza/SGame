using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.Loaders
{
    public class ContentLoader
    {
        private const string TexturesDirectory = "textures";
        private const string FontsDirectory = "fonts";

        private readonly ISystemContext systemContext;
        private Dictionary<Type, List<object>> contents = new Dictionary<Type, List<object>>();

        private ContentManager contentManager => systemContext.Game.Content;

        public ContentLoader(ISystemContext systemContext)
        {
            this.systemContext = systemContext;
        }

        public void LoadContents()
        {
            LoadContent<Texture2D>(TexturesDirectory);
            LoadContent<SpriteFont>(FontsDirectory);
        }

        private void LoadContent<T>(string directory)
        {
            var collection = contents[typeof(T)] = new List<object>();
            var fileNames = GetNamesFromDirectory(directory);

            foreach (var fileName in fileNames)
            {
                var texture = contentManager.Load<T>(fileName);
                collection.Add(texture);
            }

            LogInfoAboutLoadedContent<T>(fileNames);
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.Loaders
{
    public class ContentLoader
    {
        private const string TexturesDirectory = "textures";
        private const string FontsDirectory = "fonts";

        private readonly ISystemContext _systemContext;

        private List<Texture2D> textures = new List<Texture2D>();

        public ContentLoader(ISystemContext systemContext)
        {
            _systemContext = systemContext;
        }

        public void LoadContents()
        {
            LoadTextures();
            System.Console.WriteLine($"Loaded {textures.Count} textures!: {{ {string.Join(", ", textures)} }}");
        }

        private void LoadTextures() {
            var fileNames = GetNamesFromDirectory(TexturesDirectory);
            System.Console.WriteLine($"fileNames: {string.Join(",", fileNames)}");
            foreach(var fileName in fileNames) {
                var texture = _systemContext.Game.Content.Load<Texture2D>(fileName);
                textures.Add(texture);
            }
        }

        private IEnumerable<string> GetNamesFromDirectory(string path) {
            var combinedPath = Path.Combine(@".\", _systemContext.Game.Content.RootDirectory, path);
            var filePaths = Directory.GetFiles(combinedPath);
            var fileNames = filePaths.Select(x => Path.Combine(TexturesDirectory, Path.GetFileNameWithoutExtension(x)));
            return fileNames;
        }
    }
}
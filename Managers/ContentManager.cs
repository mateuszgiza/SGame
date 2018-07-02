using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SGame.Common.Names;
using SGame.Loaders;

namespace SGame.Managers
{
    public class ContentManager
    {
        private readonly ContentLoader contentLoader;

        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;

        public ContentManager(ContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
        }

        public void LoadContents()
        {
            textures = contentLoader.LoadContent<Texture2D>(Textures.Directory);
            fonts = contentLoader.LoadContent<SpriteFont>(Fonts.Directory);
        }

        public Texture2D GetTexture(string name)=> textures[name];
        public SpriteFont GetFont(string name) => fonts[name];
    }
}
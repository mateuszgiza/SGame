using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.Common
{
    public class LayerSettings
    {
        public Color ClearColor { get; set; }
        public string RenderTargetName { get; set; }
        public SpriteSortMode SpriteSortMode { get; set; }
        public BlendState BlendState { get; set; }
        public bool IgnoreLight { get; set; }
    }
}
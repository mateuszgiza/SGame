using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.Managers
{
    public class RenderTargetsContainer
    {
        private Dictionary<string, RenderTarget2D> renderTargets = new Dictionary<string, RenderTarget2D>();

        private GraphicsDevice graphicsDevice;

        public RenderTargetsContainer(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Create(string name, int width, int height)
        {
            renderTargets[name] = new RenderTarget2D(graphicsDevice, width, height);
        }

        public RenderTarget2D GetRenderTarget(string name) => renderTargets[name];
    }
}
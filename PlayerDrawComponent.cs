using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SGame
{
    public class PlayerDrawComponent : IComponent, IDrawable
    {
        public Entity Entity { get; set; }
        private GraphicsDevice graphicsDevice;

        public int DrawOrder => 0;

        public bool Visible => true;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public PlayerDrawComponent(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Draw(GameTime gameTime)
        {
            var sb = new SpriteBatch(graphicsDevice);
            sb.Begin();
            sb.DrawCircle(Entity.Position, 16f, 16, Color.Red);
            sb.End();
        }
    }
}
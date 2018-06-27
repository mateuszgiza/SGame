using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class DrawComponentFactory
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        public T Create<T>(Func<GraphicsDevice, T> constructor) where T : IDrawable
        {
            return constructor(GraphicsDevice);
        }
    }
}
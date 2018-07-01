using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SGame
{
    public class DrawComponent : IComponent
    {
        public Entity Entity { get; set; }
        public Texture2D Texture { get; set; }
    }
}
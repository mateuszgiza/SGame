using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SGame
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public IList<IComponent> Components { get; } = new List<IComponent>();
    }
}
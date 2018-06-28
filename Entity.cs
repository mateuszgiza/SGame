using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SGame
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public EntityComponentCollection Components { get; }
        public EntityTagCollection Tags { get; }

        public Entity()
        {
            Components = new EntityComponentCollection(this);
            Tags = new EntityTagCollection();
        }
    }
}
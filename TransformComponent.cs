using Microsoft.Xna.Framework;

namespace SGame
{
    public class TransformComponent : IComponent
    {
        public Entity Entity { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
    }
}
using Microsoft.Xna.Framework;

namespace SGame
{
    public class TransformComponent : IComponent
    {
        private Vector2? originOffset;

        public Entity Entity { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Center => Position + (Size / 2);
        public Vector2 Origin
        {
            get => originOffset.HasValue ? Position + originOffset.Value : Position + Center;
            set => originOffset = value;
        }
    }
}
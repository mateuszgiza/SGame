using Microsoft.Xna.Framework;

namespace SGame
{
    public static class EntityExtensions
    {
        public static Entity WithComponent(this Entity entity, IComponent component)
        {
            entity.Components.Attach(component);
            return entity;
        }

        public static Entity WithPosition(this Entity entity, Vector2 position)
        {
            entity.Components.Get<TransformComponent>().Position = position;
            return entity;
        }

        public static Entity WithSize(this Entity entity, Vector2 size)
        {
            entity.Components.Get<TransformComponent>().Size = size;
            return entity;
        }

        public static Entity WithTag(this Entity entity, string tag)
        {
            entity.Tags.AddTag(tag);
            return entity;
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.Builders
{
    public class EntityBuilder
    {
        private readonly ISystemContext systemContext;

        private Entity entity;

        public EntityBuilder(ISystemContext systemContext)
        {
            this.systemContext = systemContext;
            entity = new Entity();
        }

        public Entity Entity => entity;

        public EntityBuilder WithComponent<T>() where T : IComponent, new()
            => WithComponent(new T());

        public EntityBuilder WithComponent(IComponent component)
        {
            entity.Components.Attach(component);
            return this;
        }

        public EntityBuilder WithPosition(float x, float y)
            => WithPosition(new Vector2(x, y));

        public EntityBuilder WithPosition(Vector2 position)
        {
            GetComponentOrAttachIfNeeded<TransformComponent>().Position = position;
            return this;
        }

        public EntityBuilder WithSize(float x, float y)
            => WithSize(new Vector2(x, y));

        public EntityBuilder WithSize(Vector2 size)
        {
            GetComponentOrAttachIfNeeded<TransformComponent>().Size = size;
            return this;
        }

        public EntityBuilder WithTag(string tag)
        {
            entity.Tags.AddTag(tag);
            return this;
        }

        public EntityBuilder WithTexture(string name)
        {
            var texture = systemContext.ContentManager.GetTexture(name);
            return WithTexture(texture);
        }

        public EntityBuilder WithTexture(Texture2D texture)
        {
            GetComponentOrAttachIfNeeded<DrawComponent>().Texture = texture;
            return this;
        }

        private T GetComponentOrAttachIfNeeded<T>() where T : IComponent, new()
        {
            var component = entity.Components.Get<T>();
            if (component == null)
            {
                component = new T();
                WithComponent(component);
            }

            return component;
        }
    }
}
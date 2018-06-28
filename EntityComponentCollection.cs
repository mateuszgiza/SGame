using System.Collections.Generic;
using System.Linq;

namespace SGame
{
    public class EntityComponentCollection
    {
        private List<IComponent> components = new List<IComponent>();
        private Entity entity;

        public EntityComponentCollection(Entity entity)
        {
            this.entity = entity;
        }

        public T Get<T>() where T : IComponent
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public void Attach(IComponent component)
        {
            if (!components.Contains(component))
            {
                components.Add(component);
                component.Entity = entity;
            }
        }

        public void Detach(IComponent component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);
                component.Entity = null;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SGame
{
    public class SystemManager
    {
        private Dictionary<Entity, IList<IComponent>> entityComponents = new Dictionary<Entity, IList<IComponent>>();
        private Dictionary<IComponent, IList<Entity>> componentEntities = new Dictionary<IComponent, IList<Entity>>();
        private IComponentsPool componentsPool;

        public SystemManager(IComponentsPool componentsPool)
        {
            this.componentsPool = componentsPool;
        }

        public void Attach<T>(Entity entity) where T : IComponent
        {
            var component = componentsPool.GetComponent<T>();

            if (!entityComponents.TryGetValue(entity, out var components))
            {
                components = new List<IComponent>();
                entityComponents.TryAdd(entity, components);
            }

            if (!componentEntities.TryGetValue(component, out var entities))
            {
                entities = new List<Entity>();
                componentEntities.TryAdd(component, entities);
            }

            if (!components.OfType<T>().Any())
            {
                components.Add(component);
            }

            if (!entities.Contains(entity))
            {
                entities.Add(entity);
            }
        }

        public void ProcessUpdate(GameTime gameTime)
        {

        }

        private void Process(GameTime gameTime, ProcessType type)
        {
            
        }
    }
}
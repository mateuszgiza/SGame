using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SGame
{
    public class SystemManager
    {
        private Dictionary<Entity, IList<INewComponent>> entityComponents = new Dictionary<Entity, IList<INewComponent>>();
        private Dictionary<INewComponent, IList<Entity>> componentEntities = new Dictionary<INewComponent, IList<Entity>>();
        private IComponentsPool componentsPool;

        public SystemManager(IComponentsPool componentsPool)
        {
            this.componentsPool = componentsPool;
        }

        public void Attach<T>(Entity entity) where T : INewComponent
        {
            var component = componentsPool.GetComponent<T>();

            if (!entityComponents.TryGetValue(entity, out var components))
            {
                components = new List<INewComponent>();
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
            var components = componentsPool.GetComponentsByProcessType(type);
            foreach (var component in components)
            {
                var entities = componentEntities[component];
                foreach (var entity in entities)
                {
                    component.Process
                }
            }
        }
    }
}
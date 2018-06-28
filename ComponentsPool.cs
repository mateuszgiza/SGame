using System.Collections.Generic;
using System.Linq;

namespace SGame
{
    public class ComponentsPool : IComponentsPool
    {
        private IList<IComponent> pool = new List<IComponent>();

        public T GetComponent<T>() where T : IComponent
        {
            return pool.OfType<T>().FirstOrDefault();
        }

        public T Register<T>(T component) where T : IComponent
        {
            if (!pool.Contains(component))
                pool.Add(component);
            
            return component;
        }
    }
}
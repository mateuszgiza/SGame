using System.Collections.Generic;
using System.Linq;

namespace SGame
{
    public class ComponentsPool : IComponentsPool
    {
        private IList<INewComponent> pool = new List<INewComponent>();

        public T GetComponent<T>() where T : INewComponent
        {
            return pool.OfType<T>().FirstOrDefault();
        }

        public IEnumerable<INewComponent> GetComponentsByProcessType(ProcessType type)
        {
            return pool.Where(x => x.Type == type);
        }

        public T Register<T>(T component) where T : INewComponent
        {
            if (!pool.Contains(component))
                pool.Add(component);
            
            return component;
        }
    }
}
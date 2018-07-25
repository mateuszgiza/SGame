using System.Collections.Generic;
using System.Linq;

namespace SGame
{
    public class EntityCollection
    {
        private List<Entity> entities = new List<Entity>();

        public void Add(Entity entity)
        {
            if (!entities.Contains(entity))
            {
                entities.Add(entity);
            }
        }

        public IEnumerable<Entity> GetByTag(string tag)
        {
            return entities.Where(x => x.Tags.HasTag(tag));
        }
    }
}
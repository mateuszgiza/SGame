using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class EntityComponentSystem : IHaveContext
    {
        public EntityCollection Entities { get; }
        public ISystemContext Context { get; set; }

        public EntityComponentSystem()
        {
            this.Entities = new EntityCollection();
        }
    }
}
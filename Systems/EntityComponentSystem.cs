using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Builders;

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

        public void AddEntity(Action<EntityBuilder> buildAction) {
            var builder = new EntityBuilder(Context);
            buildAction.Invoke(builder);
            Entities.Add(builder.Entity);
        }
    }
}
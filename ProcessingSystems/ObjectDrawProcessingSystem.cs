using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame.ProcessingSystems
{
    public class ObjectDrawProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Draw;

        public override void Process(GameTime gameTime)
        {
            EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.Objects, DrawObjects);
            //EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.FrontEffects, DrawLight);
        }

        private void DrawObjects(SpriteBatch spriteBatch)
        {
            var objects = EntityComponentSystem.Entities.GetByTag(Tags.Object);
            objects.ForEach(o => DrawTexture(spriteBatch, o));
        }

        private void DrawLight(SpriteBatch spriteBatch)
        {
            var objects = EntityComponentSystem.Entities.GetByTag(Tags.Object);
            foreach (var obj in objects)
            {
                var transform = obj.Components.Get<TransformComponent>();
                var light = EntityComponentSystem.Context.ContentManager.GetTexture(Common.Names.Textures.LightMask);
                var off = transform.Destination;
                off.Offset(transform.Size.X, transform.Size.Y);
                spriteBatch.Draw(light, off, Color.White);
            }
        }

        private void DrawTexture(SpriteBatch spriteBatch, Entity entity)
        {
            var transform = entity.Components.Get<TransformComponent>();
            var draw = entity.Components.Get<DrawComponent>();

            spriteBatch.Draw(draw.Texture, transform.Destination, Color.White);
        }
    }
}
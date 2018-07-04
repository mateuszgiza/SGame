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
        }

        private void DrawObjects(SpriteBatch spriteBatch)
        {
            var objects = EntityComponentSystem.Entities.GetByTag(Tags.Object);
            objects.ForEach(o => DrawTexture(spriteBatch, o));
        }

        private void DrawTexture(SpriteBatch spriteBatch, Entity entity)
        {
            var transform = entity.Components.Get<TransformComponent>();
            var draw = entity.Components.Get<DrawComponent>();

            spriteBatch.Draw(draw.Texture, transform.Destination, Color.White);
        }
    }
}
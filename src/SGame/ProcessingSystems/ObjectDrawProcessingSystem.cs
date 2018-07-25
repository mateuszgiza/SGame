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
            EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.Lights, DrawLight);
        }

        private void DrawObjects(SpriteBatch spriteBatch)
        {
            var objects = EntityComponentSystem.Entities.GetByTag(Tags.Object);
            objects.ForEach(o => DrawTexture(spriteBatch, o));
        }

        private void DrawLight(SpriteBatch spriteBatch)
        {
            var light = EntityComponentSystem.Context.ContentManager.GetTexture(Common.Names.Textures.LightMask2);
            spriteBatch.Draw(light, new Vector2(100, 100), Color.White);
            spriteBatch.Draw(light, new Vector2(350, 200), Color.Yellow);
            spriteBatch.Draw(light, new Vector2(350, 50), Color.Blue);
            spriteBatch.Draw(light, new Vector2(400, 100), Color.Red);
        }

        private void DrawTexture(SpriteBatch spriteBatch, Entity entity)
        {
            var transform = entity.Components.Get<TransformComponent>();
            var draw = entity.Components.Get<DrawComponent>();

            spriteBatch.Draw(draw.Texture, transform.Destination, Color.White);
        }
    }
}
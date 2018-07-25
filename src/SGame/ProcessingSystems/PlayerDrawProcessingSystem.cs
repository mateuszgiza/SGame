using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SGame
{
    public class PlayerDrawProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Draw;

        public override void Process(GameTime gameTime)
        {
            EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.Player, DrawPlayerBody);
        }

        private void DrawPlayerBody(SpriteBatch spriteBatch)
        {
            var player = EntityComponentSystem.Entities.GetByTag(Tags.Player).FirstOrDefault();
            var transform = player.Components.Get<TransformComponent>();
            var drawComponent = player.Components.Get<DrawComponent>();

            spriteBatch.Draw(drawComponent.Texture, transform.Destination, Color.White);
        }
    }
}
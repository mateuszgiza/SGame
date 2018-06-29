using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace SGame
{
    public class PlayerDrawProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Draw;

        public override void Process(GameTime gameTime)
        {
            var player = EntityComponentSystem.Entities.GetByTag(Tags.Player).FirstOrDefault();
            var transform = player.Components.Get<TransformComponent>();
            var layer = EntityComponentSystem.Context.DrawLayerSystem.GetLayer(Layers.Player);

            var playerOrigin = transform.Position + transform.Size / 2;

            layer.DrawCircle(playerOrigin, transform.Size.X / 2, 32, Color.Red);
        }
    }
}
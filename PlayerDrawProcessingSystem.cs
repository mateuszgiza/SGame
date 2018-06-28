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
            var layer = EntityComponentSystem.Context.DrawLayerSystem.GetLayer(Layers.Player);

            var playerOrigin = player.Position + player.Size / 2;

            layer.DrawCircle(playerOrigin, player.Size.X / 2, 32, Color.Red);
        }
    }
}
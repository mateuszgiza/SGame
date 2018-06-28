using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class PlayerInputProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Update;

        public override void Process(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            var player = EntityComponentSystem.Entities.GetByTag(Tags.Player).FirstOrDefault();

            (var x, var y) = player.Position;

            if (keyState.IsKeyDown(Keys.A)) {
                x -= 5;
            }
            if (keyState.IsKeyDown(Keys.D)) {
                x += 5;
            }
            if (keyState.IsKeyDown(Keys.W)) {
                y -= 5;
            }
            if (keyState.IsKeyDown(Keys.S)) {
                y += 5;
            }

            player.Position = new Vector2(x, y);
        }
    }
}
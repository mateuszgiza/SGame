using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class NewPlayerInputComponent : INewComponent
    {        
        public ProcessType Type => ProcessType.Update;

        public void Process(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            (var x, var y) = Entity.Position;

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

            Entity.Position = new Vector2(x, y);
        }
    }
}
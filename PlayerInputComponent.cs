using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class PlayerInputComponent : IComponent, IUpdateable
    {
        public Entity Entity { get; set; }

        public bool Enabled => true;

        public int UpdateOrder => 0;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime gameTime)
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
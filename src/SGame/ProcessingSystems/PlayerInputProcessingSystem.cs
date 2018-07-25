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
            var transform = player.Components.Get<TransformComponent>();
            var inputComponent = player.Components.Get<PlayerInputComponent>();

            var direction = Vector2.Zero;

            if (keyState.IsKeyDown(inputComponent.MoveLeft))
                direction -= Vector2.UnitX;
            if (keyState.IsKeyDown(inputComponent.MoveRight))
                direction += Vector2.UnitX;
            if (keyState.IsKeyDown(inputComponent.MoveUp))
                direction -= Vector2.UnitY;
            if (keyState.IsKeyDown(inputComponent.MoveDown))
                direction += Vector2.UnitY;

            //transform.Position += direction * inputComponent.Speed;
            transform.Position = Mouse.GetState().Position.ToVector2();            
        }
    }
}
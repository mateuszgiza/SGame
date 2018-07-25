using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class PlayerInputComponent : IComponent
    {
        public Entity Entity { get; set; }
        public float Speed { get; set; } = 5f;
        public Keys MoveLeft { get; set; } = Keys.A;
        public Keys MoveRight { get; set; } = Keys.D;
        public Keys MoveUp { get; set; } = Keys.W;
        public Keys MoveDown { get; set; } = Keys.S;
    }
}
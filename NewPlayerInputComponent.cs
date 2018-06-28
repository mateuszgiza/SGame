using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class NewPlayerInputComponent : IComponent
    {
        public Entity Entity { get; set; }
        public Vector2 Speed { get; set; }
    }
}
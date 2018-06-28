using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class EntityComponentSystem
    {
        public GraphicsDevice GraphicsDevice { get; }
        public Game Game { get; }
        public EntityCollection Entities { get; }

        public EntityComponentSystem(Game game, GraphicsDevice graphicsDevice)
        {
            this.Game = game;
            this.GraphicsDevice = graphicsDevice;
            this.Entities = new EntityCollection();
        }
    }
}
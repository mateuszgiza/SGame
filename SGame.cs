using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class SGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Entity player;
        private DrawComponentFactory drawComponentFactory;

        public SGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            player = new Entity().WithComponent(new PlayerInputComponent()).WithComponent(new PlayerDrawComponent(GraphicsDevice));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.Components.OfType<IUpdateable>().ForEach(x => x.Update(gameTime));
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            player.Components.OfType<IDrawable>().ForEach(x => x.Draw(gameTime));

            base.Draw(gameTime);
        }
    }
}
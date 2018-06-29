using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class SGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SystemContext Context;
        private FpsCounter FpsCounter;

        public SGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsFixedTimeStep = false; // unlock 60fps
            //graphics.SynchronizeWithVerticalRetrace = false; // disable vsync
        }

        protected override void Initialize()
        {
            base.Initialize();

            FpsCounter = new FpsCounter();

            Context = new SystemContext();
            Context.Game = this;
            Context.GraphicsDevice = GraphicsDevice;
            Context.SystemManager = new SystemManager();
            Context.EntityComponentSystem = new EntityComponentSystem();
            Context.ProcessingSystemManager = new ProcessingSystemManager();
            Context.DrawLayerSystem = new DrawLayerSystem();

            Context.ProcessingSystemManager
                    .Register(new PlayerInputProcessingSystem())
                    .Register(new PlayerDrawProcessingSystem());

            Context.EntityComponentSystem.Entities.Add(new Entity()
                    .WithTag(Tags.Player)
                    .WithComponent(new TransformComponent())
                    .WithSize(new Vector2(50, 50))
                    .WithPosition(new Vector2(100, 100))
                    .WithComponent(new PlayerInputComponent()));

            Context.DrawLayerSystem
                    .AddLayer(Layers.FpsCounter)
                    .AddLayer(Layers.Player);
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Context.SystemManager.ProcessUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            FpsCounter.Update(gameTime);
            System.Console.WriteLine($"FPS: {FpsCounter.FramesCount}");

            Context.SystemManager.ProcessDraw(gameTime);

            base.Draw(gameTime);
        }
    }
}
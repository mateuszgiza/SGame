using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SGame.Common.Names;
using SGame.Loaders;
using SGame.Managers;
using SGame.ProcessingSystems;

namespace SGame
{
    public class SGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SystemContext Context;

        public SGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsFixedTimeStep = false; // unlock 60fps
            //graphics.SynchronizeWithVerticalRetrace = false; // disable vsync

            PreInit();
        }

        private void PreInit()
        {
            var contentLoader = new ContentLoader(Content);

            Context = new SystemContext { Game = this };
            Context.ContentManager = new ContentManager(contentLoader);
        }

        protected override void Initialize()
        {
            base.Initialize();

            Context.GraphicsDevice = GraphicsDevice;
            Context.SystemManager = new SystemManager();
            Context.EntityComponentSystem = new EntityComponentSystem();
            Context.ProcessingSystemManager = new ProcessingSystemManager();
            Context.DrawLayerSystem = new DrawLayerSystem();

            Context.ProcessingSystemManager
                    .Register(new PlayerInputProcessingSystem())
                    .Register(new PlayerDrawProcessingSystem())
                    .Register(new FpsCounterProcessingSystem());

            Context.EntityComponentSystem.AddEntity(_ => _
                    .WithTag(Tags.Player)
                    .WithSize(50, 50)
                    .WithPosition(100, 100)
                    .WithComponent<PlayerInputComponent>()
                    .WithComponent<DrawComponent>()
                    .WithTexture(Textures.Ball));

            Context.DrawLayerSystem
                    .AddLayer(Layers.FpsCounter)
                    .AddLayer(Layers.Player);
        }

        protected override void LoadContent()
        {
            Context.ContentManager.LoadContents();
        }

        protected override void Update(GameTime gameTime)
        {
            Context.SystemManager.ProcessUpdate(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            Context.SystemManager.ProcessDraw(gameTime);
        }
    }
}
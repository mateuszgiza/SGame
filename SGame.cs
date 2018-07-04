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

            RegisterProcessingSystems();
            CreateLayers();
            CreateEntities();
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

        private void RegisterProcessingSystems()
        {
            Context.ProcessingSystemManager
                    .Register(new PlayerInputProcessingSystem())
                    .Register(new PlayerDrawProcessingSystem())
                    .Register(new FpsCounterProcessingSystem())
                    .Register(new ObjectDrawProcessingSystem());
        }

        private void CreateLayers()
        {
            Context.DrawLayerSystem
                    .AddLayer(Layers.FpsCounter)
                    .AddLayer(Layers.Player)
                    .AddLayer(Layers.Objects);
        }

        private void CreateEntities()
        {
            Context.EntityComponentSystem.AddEntity(_ => _
                    .WithTag(Tags.Player)
                    .WithSize(50, 50)
                    .WithPosition(100, 100)
                    .WithComponent<PlayerInputComponent>()
                    .WithTexture(Textures.Ball));

            Context.EntityComponentSystem.AddEntity(_ => _
                    .WithTag(Tags.Object)
                    .WithSize(64, 64)
                    .WithPosition(200, 200)
                    .WithTexture(Textures.Brick));
        }
    }
}
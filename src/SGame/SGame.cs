using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SGame.Common.Names;
using SGame.Common.Setups;
using SGame.Components;
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

            var pp = GraphicsDevice.PresentationParameters;
            var renderTargetsContainer = new RenderTargetsContainer(GraphicsDevice);
            renderTargetsContainer.Create(RenderTargets.Main, pp.BackBufferWidth, pp.BackBufferHeight);
            renderTargetsContainer.Create(RenderTargets.FpsCounter, pp.BackBufferWidth, pp.BackBufferHeight);
            renderTargetsContainer.Create(RenderTargets.Objects, pp.BackBufferWidth, pp.BackBufferHeight);
            renderTargetsContainer.Create(RenderTargets.Player, pp.BackBufferWidth, pp.BackBufferHeight);
            renderTargetsContainer.Create(RenderTargets.Lights, pp.BackBufferWidth, pp.BackBufferHeight);

            Context.GraphicsDevice = GraphicsDevice;
            Context.SystemManager = new SystemManager();
            Context.EntityComponentSystem = new EntityComponentSystem();
            Context.ProcessingSystemManager = new ProcessingSystemManager();
            Context.DrawLayerSystem = new DrawLayerSystem(renderTargetsContainer, GraphicsDevice);

            RegisterProcessingSystems();
            CreateLayers();
            CreateEntities();

            lightBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            Context.ContentManager.LoadContents();
        }

        protected override void Update(GameTime gameTime)
        {
            CheckKeysForLockingOrUnlockingFramerate();
            Context.SystemManager.ProcessUpdate(gameTime);
        }

        private SpriteBatch lightBatch;

        protected override void Draw(GameTime gameTime)
        {
            Context.SystemManager.ProcessDraw(gameTime);

            Context.DrawLayerSystem.RenderSpecifiedTargets(
                RenderTargets.Objects,
                RenderTargets.Player,
                RenderTargets.FpsCounter
            );
        }

        private void RegisterProcessingSystems()
        {
            Context.ProcessingSystemManager
                    .Register(new PlayerInputProcessingSystem())
                    .Register(new PlayerDrawProcessingSystem())
                    .Register(new FpsCounterProcessingSystem())
                    .Register(new FpsCounterDrawProcessingSystem())
                    .Register(new ObjectDrawProcessingSystem());
        }

        private void CreateLayers()
        {
            Context.DrawLayerSystem
                    .AddLayer(Layers.Lights, LayerSetups.Lights)
                    .AddLayer(Layers.FpsCounter, LayerSetups.FpsCounter)
                    .AddLayer(Layers.Player, LayerSetups.Player)
                    .AddLayer(Layers.Objects, LayerSetups.Objects);
            //.AddLayer(Layers.FrontEffects)
            //.AddLayer(Layers.BackEffects);
        }

        private void CreateEntities()
        {
            Context.EntityComponentSystem.AddEntity(_ => _
                    .WithTag(Tags.FpsCounter)
                    .WithComponent<FpsCounterComponent>());

            Context.EntityComponentSystem.AddEntity(_ => _
                    .WithTag(Tags.Player)
                    .WithSize(50, 50)
                    .WithPosition(100, 100)
                    .WithComponent<PlayerInputComponent>()
                    .WithTexture(Textures.Ball));

            for (int x = 0; x < GraphicsDevice.PresentationParameters.BackBufferWidth; x += 64)
                for (int y = 0; y < GraphicsDevice.PresentationParameters.BackBufferWidth; y += 64)
                    Context.EntityComponentSystem.AddEntity(_ => _
                        .WithTag(Tags.Object)
                        .WithSize(64, 64)
                        .WithPosition(x, y)
                        .WithTexture(Textures.Brick));
        }

        private void CheckKeysForLockingOrUnlockingFramerate()
        {
            var keyState = Keyboard.GetState();
            var isUnlock = keyState.IsKeyDown(Keys.O);
            var isLock = keyState.IsKeyDown(Keys.L);

            if (isLock || isUnlock)
            {
                IsFixedTimeStep = isLock; // unlock 60fps
                graphics.SynchronizeWithVerticalRetrace = isLock; // disable vsync
                graphics.ApplyChanges();
            }
        }
    }
}
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

        private Effect lightEffect;
        private RenderTarget2D lightsTarget;
        private RenderTarget2D mainTarget;

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

            lightBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            Context.ContentManager.LoadContents();

            lightEffect = Content.Load<Effect>("shaders/light");

            var pp = GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            Context.SystemManager.ProcessUpdate(gameTime);
        }

        private SpriteBatch lightBatch;

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(lightsTarget);
            graphics.GraphicsDevice.Clear(Color.Black);

            lightBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            lightBatch.Draw(Context.ContentManager.GetTexture(Textures.LightMask2), Vector2.One * 100, Color.White);
            lightBatch.End();

            GraphicsDevice.SetRenderTarget(mainTarget);
            GraphicsDevice.Clear(Color.Black);
            Context.SystemManager.ProcessDraw(gameTime);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            var spriteBatch = Context.DrawLayerSystem.GetLayer(Layers.Objects);

            lightEffect.Parameters["lightMask"].SetValue(lightsTarget);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            lightEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
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
                    .AddLayer(Layers.Objects)
                    .AddLayer(Layers.FrontEffects)
                    .AddLayer(Layers.BackEffects);
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
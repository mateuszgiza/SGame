using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Managers;

namespace SGame
{
    public class SystemContext : ISystemContext
    {
        private SystemManager systemManager;
        private ProcessingSystemManager processingSystemManager;
        private EntityComponentSystem entityComponentSystem;
        private DrawLayerSystem drawLayerSystem;

        public Game Game { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public ContentManager ContentManager { get; set; }

        public SystemManager SystemManager
        {
            get => systemManager;
            set => SetContextWithProperty(ref systemManager, value);
        }
        public ProcessingSystemManager ProcessingSystemManager
        {
            get => processingSystemManager;
            set => SetContextWithProperty(ref processingSystemManager, value);
        }

        public EntityComponentSystem EntityComponentSystem
        {
            get => entityComponentSystem;
            set => SetContextWithProperty(ref entityComponentSystem, value);
        }

        public DrawLayerSystem DrawLayerSystem
        {
            get => drawLayerSystem;
            set => SetContextWithProperty(ref drawLayerSystem, value);
        }

        private void SetContextWithProperty<T>(ref T property, T value) where T : IHaveContext
        {
            value.Context = this;
            property = value;
        }
    }
}
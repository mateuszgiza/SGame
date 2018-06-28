using Microsoft.Xna.Framework;

namespace SGame
{
    public abstract class ProcessingSystem : IProcessingSystem
    {
        internal EntityComponentSystem EntityComponentSystem;

        public abstract ProcessType Type {get;}

        public abstract void Process(GameTime gameTime);
    }
}
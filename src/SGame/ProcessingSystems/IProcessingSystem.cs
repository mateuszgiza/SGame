using Microsoft.Xna.Framework;

namespace SGame
{
    public interface IProcessingSystem
    {
        ProcessType Type { get; }
        void Process(GameTime gameTime);
    }
}
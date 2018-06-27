using Microsoft.Xna.Framework;

namespace SGame
{
    public interface IComponent
    {
        Entity Entity { get; set; }
    }

    public interface INewComponent
    {
        ProcessType Type { get; }
        void Process(GameTime gameTime);
    }
}
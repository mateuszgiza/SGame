using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public interface ISystemContext
    {
        Game Game { get; }
        GraphicsDevice GraphicsDevice { get; }
        SystemManager SystemManager { get; }
        ProcessingSystemManager ProcessingSystemManager { get; }
        EntityComponentSystem EntityComponentSystem { get; }
        DrawLayerSystem DrawLayerSystem { get; }
    }
}
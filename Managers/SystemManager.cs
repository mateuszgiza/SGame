using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class SystemManager : IHaveContext
    {
        public ISystemContext Context { get; set; }

        public void ProcessUpdate(GameTime gameTime)
        {
            Context.ProcessingSystemManager.ProcessSystems(gameTime, ProcessType.Update);
        }

        public void ProcessDraw(GameTime gameTime)
        {
            Context.ProcessingSystemManager.ProcessSystems(gameTime, ProcessType.Draw);

            Context.DrawLayerSystem.DrawEntireLayer(Layers.Player);
        }
    }
}
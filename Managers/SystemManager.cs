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
            var layer = Context.DrawLayerSystem.GetLayer(Layers.Player);

            layer.Begin();
            Context.ProcessingSystemManager.ProcessSystems(gameTime, ProcessType.Draw);
            layer.End();
        }
    }
}
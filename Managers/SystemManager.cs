using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SGame
{
    public class SystemManager : IHaveContext
    {
        public ISystemContext Context { get; set; }

        public void ProcessUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Context.Game.Exit();
            else
                Context.ProcessingSystemManager.ProcessSystems(gameTime, ProcessType.Update);
        }

        public void ProcessDraw(GameTime gameTime)
        {
            Context.ProcessingSystemManager.ProcessSystems(gameTime, ProcessType.Draw);
            Context.DrawLayerSystem.DrawEntireLayer(Layers.FpsCounter);
            Context.DrawLayerSystem.DrawEntireLayer(Layers.Player);
            Context.DrawLayerSystem.DrawEntireLayer(Layers.Objects);
        }
    }
}
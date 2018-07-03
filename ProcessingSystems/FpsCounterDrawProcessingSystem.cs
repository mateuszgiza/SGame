using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Common.Names;

namespace SGame.ProcessingSystems
{
    public class FpsCounterProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Draw;

        private FpsCounter fpsCounter = new FpsCounter();

        public override void Process(GameTime gameTime)
        {
            fpsCounter.Update(gameTime);
            EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.FpsCounter, DrawCounter);
        }

        private void DrawCounter(SpriteBatch spriteBatch)
        {
            var font = EntityComponentSystem.Context.ContentManager.GetFont(Fonts.Default);
            spriteBatch.DrawString(font, $"FPS: {fpsCounter.FramesCount}", Vector2.Zero, Color.Magenta);
        }
    }
}
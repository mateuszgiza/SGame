using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Common.Names;
using SGame.Components;

namespace SGame.ProcessingSystems
{
    public class FpsCounterDrawProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Draw;

        public override void Process(GameTime gameTime)
        {
            EntityComponentSystem.Entities
                .GetByTag(Tags.FpsCounter)?.FirstOrDefault()
                ?.Components.Get<FpsCounterComponent>()
                .DrawCounter.Update(gameTime);

            EntityComponentSystem.Context.DrawLayerSystem.DrawOnLayer(Layers.FpsCounter, DrawCounter);
        }

        private void DrawCounter(SpriteBatch spriteBatch)
        {
            var counter = EntityComponentSystem.Entities
                .GetByTag(Tags.FpsCounter)?.FirstOrDefault()
                ?.Components.Get<FpsCounterComponent>();

            if (counter == null)
                return;

            var text = $"FPS: {counter.DrawCounter.FramesCount}\nUpdate: {counter.UpdateCounter.FramesCount}";
            var font = EntityComponentSystem.Context.ContentManager.GetFont(Fonts.Default);
            
            spriteBatch.DrawString(font, text, Vector2.Zero, Color.Magenta);
        }
    }
}
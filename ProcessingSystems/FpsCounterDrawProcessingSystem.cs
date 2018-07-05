using System.Linq;
using System.Text;
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

            var textBuilder = new StringBuilder();
            textBuilder.AppendLine($"FPS: {counter.DrawCounter.FramesCount}");
            textBuilder.AppendLine($"Update: {counter.UpdateCounter.FramesCount}");
            textBuilder.AppendLine($"FixedUpdate: {counter.FixedUpdateCounter.FramesCount}");
            
            var font = EntityComponentSystem.Context.ContentManager.GetFont(Fonts.Default);
            spriteBatch.DrawString(font, textBuilder, Vector2.Zero, Color.Magenta);
        }
    }
}
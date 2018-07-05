using System.Linq;
using Microsoft.Xna.Framework;
using SGame.Components;

namespace SGame.ProcessingSystems
{
    public class FpsCounterProcessingSystem : ProcessingSystem
    {
        public override ProcessType Type => ProcessType.Update;

        public override void Process(GameTime gameTime)
        {
            EntityComponentSystem.Entities
                .GetByTag(Tags.FpsCounter)?.FirstOrDefault()
                ?.Components.Get<FpsCounterComponent>()
                .UpdateCounter.Update(gameTime);
        }
    }
}
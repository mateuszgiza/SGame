using System;
using Microsoft.Xna.Framework;

namespace SGame
{
    public class FpsCounter
    {
        private int frameCounter;
        private int lastFramesCount;

        private TimeSpan elapsedTime;
        private static TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        public TimeSpan MaxElapsedTime { get; set; } = OneSecond;

        public int FramesCount => lastFramesCount;

        public void Update(GameTime gameTime)
        {
            frameCounter++;
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime >= MaxElapsedTime)
            {
                elapsedTime -= MaxElapsedTime;
                lastFramesCount = frameCounter;
                frameCounter = 0;
            }
        }
    }
}
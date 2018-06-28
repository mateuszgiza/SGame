using System;
using Microsoft.Xna.Framework;

namespace SGame
{
    public class FpsCounter
    {
        private int frameCounter;
        private int lastFramesCount;

        private TimeSpan elapsedTime;
        private TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        public int FramesCount => lastFramesCount;

        public void Update(GameTime gameTime)
        {
            frameCounter++;
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime >= OneSecond)
            {
                elapsedTime -= OneSecond;
                lastFramesCount = frameCounter;
                frameCounter = 0;
            }
        }
    }
}
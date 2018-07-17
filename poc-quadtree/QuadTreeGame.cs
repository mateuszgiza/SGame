using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace poc_quadtree
{
    public class QuadTreeGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch sprite;
        private List<Rectangle> net = new List<Rectangle>();
        private Rectangle? collisionRect;

        public QuadTreeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            sprite = new SpriteBatch(GraphicsDevice);

            int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            const int QuadSize = 32;

            for (int x = 0; x < width; x += QuadSize)
                for (int y = 0; y < height; y += QuadSize)
                    net.Add(new Rectangle(x, y, QuadSize, QuadSize));
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();
            else
                CheckCollisions();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            sprite.Begin();

            foreach (var netItem in net)
            {
                var isTheSameRect = collisionRect == netItem;
                var color = isTheSameRect ? Color.Red : Color.FromNonPremultiplied(255, 255, 255, 25);
                sprite.DrawRectangle(netItem, color);
            }

            sprite.End();
        }

        private void CheckCollisions()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = mouseState.Position;
            var mouseRect = new Rectangle(mousePosition, Point.Zero);

            foreach (var netItem in net)
                if (netItem.Intersects(mouseRect))
                    collisionRect = netItem;
        }
    }
}
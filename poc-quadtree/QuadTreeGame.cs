using System;
using System.Linq;
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
        private QuadTree points;
        private QuadTreeRect objects;
        private IEnumerable<Vector2> selectedPoints;
        private IEnumerable<Rectangle> selectedObjects;
        private List<Vector2> allPoints = new List<Vector2>();
        private List<Rectangle> allRects = new List<Rectangle>();

        public QuadTreeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var rand = new Random(2137_2137);

            sprite = new SpriteBatch(GraphicsDevice);

            int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            const int QuadSize = 16;
            const int MaxPoints = 2048;

            var windowBoundaries = new Rectangle(0, 0, width, height);
            points = new QuadTree(windowBoundaries);
            objects = new QuadTreeRect(windowBoundaries);

            for (int x = 0; x < width; x += QuadSize)
                for (int y = 0; y < height; y += QuadSize)
                    net.Add(new Rectangle(x, y, QuadSize, QuadSize));

            for (int i = 0; i < MaxPoints; ++i)
            {
                var x = rand.Next(0, width);
                var y = rand.Next(0, height);
                var point = new Vector2(x, y);
                points.Insert(point);
                allPoints.Add(point);
            }

            for (int i = 0; i < MaxPoints/2; ++i)
            {
                var x = rand.Next(0, width);
                var y = rand.Next(0, height);
                var rect = new Rectangle(x, y, 5, 5);
                objects.Insert(rect);
                allRects.Add(rect);
            }

            for (int i = 0; i < MaxPoints/2; ++i)
            {
                var x = rand.Next(0, width);
                var y = rand.Next(0, height);
                var rect = new Rectangle(x, y, 20, 20);
                objects.Insert(rect);
                allRects.Add(rect);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();
            else
                CheckCollisions();
        }

        private Color selectedColor = Color.Red;
        private Color normalColor = Color.FromNonPremultiplied(255, 255, 255, 25);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            sprite.Begin();

            foreach (var netItem in net)
            {
                var isTheSameRect = collisionRect == netItem;
                var color = isTheSameRect ? selectedColor : normalColor;
                sprite.DrawRectangle(netItem, color);
            }

            foreach (var p in allPoints)
            {
                sprite.DrawPoint(p, Color.DarkBlue, 2);
            }

            foreach (var r in allRects)
            {
                sprite.DrawRectangle(r, Color.DarkBlue, 1);
            }

            foreach (var point in selectedPoints)
            {
                sprite.DrawPoint(point, Color.GreenYellow, 2);
            }

            foreach (var obj in selectedObjects)
            {
                sprite.DrawRectangle(obj, Color.OrangeRed, 1);
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
                {
                    collisionRect = netItem;
                    break;
                }

            if (collisionRect.HasValue)
            {
                selectedPoints = points.Query(collisionRect.Value);
                selectedObjects = objects.Query(collisionRect.Value);
            }
            else
            {
                selectedPoints = Enumerable.Empty<Vector2>();
                selectedObjects = Enumerable.Empty<Rectangle>();
            }
        }
    }

    public class QuadTree
    {
        private const int MaxNodeCapacity = 4;
        private int freeIndex = 0;
        private List<Vector2> points = new List<Vector2>(MaxNodeCapacity);
        private Rectangle boundary;

        private QuadTree TopLeft, TopRight, BottomLeft, BottomRight;

        public QuadTree(Rectangle boundary)
            => this.boundary = boundary;

        public bool Insert(Vector2 point)
        {
            if (!boundary.Contains(point))
                return false;

            if (points.Count < MaxNodeCapacity)
            {
                points.Add(point);
                return true;
            }

            if (TopLeft == null)
                Subdivide();

            if (TopLeft.Insert(point)) return true;
            if (TopRight.Insert(point)) return true;
            if (BottomLeft.Insert(point)) return true;
            if (BottomRight.Insert(point)) return true;

            return false;
        }

        public IEnumerable<Vector2> Query(Rectangle bounds)
        {
            var list = new List<Vector2>();

            if (!boundary.Intersects(bounds))
                return list;

            for (int i = 0; i < points.Count; ++i)
            {
                if (bounds.Contains(points[i]))
                    list.Add(points[i]);
            }

            if (TopLeft == null)
                return list;

            list.AddRange(TopLeft.Query(bounds));
            list.AddRange(TopRight.Query(bounds));
            list.AddRange(BottomLeft.Query(bounds));
            list.AddRange(BottomRight.Query(bounds));

            return list;
        }

        private void Subdivide()
        {
            var x = boundary.X;
            var y = boundary.Y;
            var halfWidth = boundary.Width / 2;
            var halfHeight = boundary.Height / 2;

            TopLeft = new QuadTree(new Rectangle(x, y, halfWidth, halfHeight));
            TopRight = new QuadTree(new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
            BottomLeft = new QuadTree(new Rectangle(x, y + halfHeight, halfWidth, halfHeight));
            BottomRight = new QuadTree(new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
        }
    }

    public class QuadTreeRect
    {
        private const int MaxNodeCapacity = 4;
        private int freeIndex = 0;
        private List<Rectangle> objects = new List<Rectangle>(MaxNodeCapacity);
        private Rectangle boundary;

        private QuadTreeRect TopLeft, TopRight, BottomLeft, BottomRight;

        public QuadTreeRect(Rectangle boundary)
            => this.boundary = boundary;

        public void Insert(Rectangle rect)
        {
            if (!boundary.Intersects(rect))
                return;

            if (objects.Count < MaxNodeCapacity)
            {
                objects.Add(rect);
                return;
            }

            if (TopLeft == null)
                Subdivide();
            
            TopLeft.Insert(rect);
            TopRight.Insert(rect);
            BottomLeft.Insert(rect);
            BottomRight.Insert(rect);
        }

        public IEnumerable<Rectangle> Query(Rectangle bounds)
        {
            var list = new List<Rectangle>();

            if (!boundary.Intersects(bounds))
                return list;

            for (int i = 0; i < objects.Count; ++i)
            {
                if (bounds.Intersects(objects[i]))
                    list.Add(objects[i]);
            }

            if (TopLeft == null)
                return list;

            list.AddRange(TopLeft.Query(bounds));
            list.AddRange(TopRight.Query(bounds));
            list.AddRange(BottomLeft.Query(bounds));
            list.AddRange(BottomRight.Query(bounds));

            return list;
        }

        private void Subdivide()
        {
            var x = boundary.X;
            var y = boundary.Y;
            var halfWidth = boundary.Width / 2;
            var halfHeight = boundary.Height / 2;

            TopLeft = new QuadTreeRect(new Rectangle(x, y, halfWidth, halfHeight));
            TopRight = new QuadTreeRect(new Rectangle(x + halfWidth, y, halfWidth, halfHeight));
            BottomLeft = new QuadTreeRect(new Rectangle(x, y + halfHeight, halfWidth, halfHeight));
            BottomRight = new QuadTreeRect(new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
        }
    }
}
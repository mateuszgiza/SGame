using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class DrawLayerSystem : IHaveContext
    {
        public delegate void DrawAction(SpriteBatch spriteBatch);

        private Dictionary<string, SpriteBatch> layers = new Dictionary<string, SpriteBatch>();
        private Dictionary<string, List<DrawAction>> drawActions = new Dictionary<string, List<DrawAction>>();

        public ISystemContext Context { get; set; }

        public DrawLayerSystem AddLayer(string layerName)
        {
            if (!layers.ContainsKey(layerName))
            {
                layers[layerName] = new SpriteBatch(Context.GraphicsDevice);
                drawActions[layerName] = new List<DrawAction>();
            }
            else
            {
                throw new LayerAlreadyExistsException(layerName);
            }

            return this;
        }

        public SpriteBatch GetLayer(string layerName)
        {
            return layers[layerName];
        }

        public void DrawEntireLayer(string layerName)
        {
            var layer = GetLayer(layerName);
            layer.Begin();

            foreach (var drawAction in drawActions[layerName])
            {
                drawAction.Invoke(layer);
            }

            layer.End();

            drawActions[layerName].Clear();
        }

        public void DrawOnLayer(string layer, DrawAction drawAction)
        {
            drawActions[layer].Add(drawAction);
        }
    }
}
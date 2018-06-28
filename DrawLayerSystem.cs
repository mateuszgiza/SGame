using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SGame
{
    public class DrawLayerSystem : IHaveContext
    {
        private Dictionary<string, SpriteBatch> layers = new Dictionary<string, SpriteBatch>();

        public ISystemContext Context { get; set; }

        public DrawLayerSystem AddLayer(string layerName)
        {
            if (!layers.ContainsKey(layerName))
            {
                layers[layerName] = new SpriteBatch(Context.GraphicsDevice);
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
    }
}
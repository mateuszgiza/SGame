using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Common;
using SGame.Common.Names;
using SGame.Managers;

namespace SGame
{
    public class DrawLayerSystem : IHaveContext
    {
        public delegate void DrawAction(SpriteBatch spriteBatch);

        private Dictionary<string, LayerSettings> layers = new Dictionary<string, LayerSettings>();
        private Dictionary<string, List<DrawAction>> drawActions = new Dictionary<string, List<DrawAction>>();

        private SpriteBatch spriteBatch;
        private readonly RenderTargetsContainer renderTargetsContainer;

        public ISystemContext Context { get; set; }

        public DrawLayerSystem(RenderTargetsContainer renderTargetsContainer, GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.renderTargetsContainer = renderTargetsContainer;
        }

        public DrawLayerSystem AddLayer(string layerName, LayerSettings layerSettings)
        {
            if (!layers.ContainsKey(layerName))
            {
                layers[layerName] = layerSettings;
                drawActions[layerName] = new List<DrawAction>();
            }
            else
            {
                throw new LayerAlreadyExistsException(layerName);
            }

            return this;
        }

        public LayerSettings GetLayer(string layerName) => layers[layerName];

        public void DrawEntireLayer(string layerName)
        {
            var layer = GetLayer(layerName);
            var renderTarget = renderTargetsContainer.GetRenderTarget(layer.RenderTargetName);

            Context.GraphicsDevice.SetRenderTarget(renderTarget);
            Context.GraphicsDevice.Clear(layer.ClearColor);

            spriteBatch.Begin(layer.SpriteSortMode, layer.BlendState);

            foreach (var drawAction in drawActions[layerName])
            {
                drawAction.Invoke(spriteBatch);
            }

            spriteBatch.End();

            drawActions[layerName].Clear();
        }

        public void RenderSpecifiedTargets(params string[] targetNames)
        {
            Context.GraphicsDevice.SetRenderTarget(null);
            Context.GraphicsDevice.Clear(Color.Transparent);

            targetNames.ForEach(RenderTarget);
        }

        public void RenderTarget(string targetName)
        {
            var renderTarget = renderTargetsContainer.GetRenderTarget(targetName);

            var layerOptions = GetLayer(targetName);
            if (layerOptions?.IgnoreLight ?? false)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            }
            else
            {
                var lightEffect = Context.ContentManager.GetEffect(Effects.Light);
                var lightsRenderTarget = renderTargetsContainer.GetRenderTarget(RenderTargets.Lights);
                
                lightEffect.Parameters["lightMask"].SetValue(lightsRenderTarget);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                lightEffect.CurrentTechnique.Passes[0].Apply();
            }

            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public void DrawOnLayer(string layer, DrawAction drawAction)
        {
            drawActions[layer].Add(drawAction);
        }
    }
}
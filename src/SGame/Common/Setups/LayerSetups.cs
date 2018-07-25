using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGame.Common.Names;

namespace SGame.Common.Setups
{
    public static class LayerSetups
    {
        public static LayerSettings FpsCounter { get; } = new LayerSettings
        {
            ClearColor = Color.Transparent,
            RenderTargetName = RenderTargets.FpsCounter,
            BlendState = null,
            SpriteSortMode = SpriteSortMode.Deferred,
            IgnoreLight = true
        };

        public static LayerSettings Lights { get; } = new LayerSettings
        {
            ClearColor = Color.FromNonPremultiplied(25,25,25,255),
            RenderTargetName = RenderTargets.Lights,
            BlendState = BlendState.Additive,
            SpriteSortMode = SpriteSortMode.Immediate
        };

        public static LayerSettings Player { get; } = new LayerSettings
        {
            ClearColor = Color.Transparent,
            RenderTargetName = RenderTargets.Player,
            BlendState = null,
            SpriteSortMode = SpriteSortMode.Deferred
        };

        public static LayerSettings Objects { get; } = new LayerSettings
        {
            ClearColor = Color.Transparent,
            RenderTargetName = RenderTargets.Objects,
            BlendState = null,
            SpriteSortMode = SpriteSortMode.Deferred
        };
    }
}
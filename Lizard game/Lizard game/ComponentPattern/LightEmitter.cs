using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class LightEmitter : Component
    {
        private float radius;
        private RenderTarget2D shadowTarget;
        private static Effect shaderShadowEffect;


        public float Radius { get => radius; set => radius = value; }
        public float X { get => GameObject.Transform.Position.X / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth; }
        public float Y { get => GameObject.Transform.Position.Y / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight; }
        public static Effect ShaderShadowEffect { get => shaderShadowEffect; set => shaderShadowEffect = value; }

        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            this.Radius = radius;
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            shadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferHeight, device.PresentationParameters.BackBufferHeight);
        }

        public float NormalizedDistanceToLight(ShadowCaster shadow)
        {
            return ((shadow.GameObject.Transform.Position - GameObject.Transform.Position) / (Radius * GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth * 2)).Length();
        }


        /// <summary>
        /// Using a list of shadowIntervals, it draws the corrosponnding shadows to the lights RenderTarget.
        /// </summary>
        public void DrawShadowsToTarget(SpriteBatch spriteBatch, List<ShadowInterval> shadows)
        {
            //shaderShadowEffect.Parameters["lightPositions"].SetValue(new Vector2(X,Y));

            GameWorld.Instance.GraphicsDevice.SetRenderTarget(shadowTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: ShaderShadowEffect);
            foreach (ShadowInterval shadow in shadows)
            {
                spriteBatch.Draw(GameWorld.Instance.Pixel, shadowTarget.Bounds, shadow.ToDataPass());
            }
            spriteBatch.End();
        }

        public void DrawToLightMask(SpriteBatch spriteBatch)
        {
            float resizedRadius = Radius * GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth * 2;
            Vector2 size = new Vector2((int)resizedRadius, (int)resizedRadius);
            Point position = (GameObject.Transform.Position - (size / 2)).ToPoint();
            spriteBatch.Draw(shadowTarget, new Rectangle(position, size.ToPoint()), Color.White);

            //spriteBatch.Draw(shadowTarget, new Vector2(0, 0), Color.White);
        }
    }
}

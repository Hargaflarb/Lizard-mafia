using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public struct ShadowInterval
    {
        private float upperAngle;
        private float lowerAngle;
        private float angleOffset;
        private float distance;

        public ShadowInterval(ShadowCaster shadowCaster, LightEmitter light)
        {
            distance = shadowCaster.NormalizedDistanceToLight(light);
            float nondistance = shadowCaster.CalculateDistanceToLight(light);
            float BaseAngle = shadowCaster.CalculateLightToShadowAngle(light);
            float AngleIntervalSize = shadowCaster.CalculateAngle(nondistance);
            if (BaseAngle + AngleIntervalSize > MathF.PI * 2)
            {
                angleOffset = -((MathF.PI * 2) % (BaseAngle + AngleIntervalSize));
            }
            else if (BaseAngle - AngleIntervalSize < 0)
            {
                // double negativity
                angleOffset = -(BaseAngle - AngleIntervalSize);
            }
            else
            {
                angleOffset = 0;
            }
            upperAngle = BaseAngle + AngleIntervalSize;// + angleOffset;
            lowerAngle = BaseAngle - AngleIntervalSize;// + angleOffset;
        }

        public float UpperAngle { get => upperAngle; }
        public float LowerAngle { get => lowerAngle; }
        public float AngleOffset { get => angleOffset; }
        public float Distance { get => distance; }

        public Color ToDataPass()
        {
            return new Color(upperAngle, lowerAngle, angleOffset, distance);
        }
    }

    public static class ShadowMap
    {
        private static Texture2D shadowSprite;
        private static Effect shaderEffect;
        private static Effect shaderShadowEffect;
        private static RenderTarget2D lightTarget;
        private static RenderTarget2D shadowTarget;
        private static Color color = Color.White;


        public static Texture2D ShadowSprite { get => shadowSprite; set => shadowSprite = value; }
        public static Effect ShaderEffect { get => shaderEffect; set => shaderEffect = value; }
        public static Effect ShaderShadowEffect { get => shaderShadowEffect; set => shaderShadowEffect = value; }
        public static RenderTarget2D LightTarget { get => lightTarget; set => lightTarget = value; }
        public static RenderTarget2D ShadowTarget { get => shadowTarget; set => shadowTarget = value; }
        public static Color Color { get => color; set => color = value; }

        static ShadowMap()
        {
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            LightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            ShadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
        }


        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public static void SetSprite()
        {
            ShadowSprite = GameWorld.Instance.Content.Load<Texture2D>("shadow");
            shaderEffect = GameWorld.Instance.Content.Load<Effect>("TestShader");
            ShaderShadowEffect = GameWorld.Instance.Content.Load<Effect>("TestShaderShadow");
            //ShadowMapSprite = GameWorld.Instance.Content.Load<Texture2D>("shadowMap");
        }

        public static void PrepareShadows(SpriteBatch spriteBatch)
        {


            Vector2[] hi = new Vector2[] { GameWorld.Instance.PlayerObject.Transform.Position / GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2(), new Vector2(0.4f, 0.4f), Vector2.One * 2, Vector2.One * 2, new Vector2(0.1f, 0.7f) };
            float[] bye = new float[hi.Length * 2];
            for (int i = 0; i < hi.Length; i++)
            {
                bye[i * 2] = hi[i].X;
                bye[i * 2 + 1] = hi[i].Y;
            }
            //shaderEffect.Parameters["lightPositions"].SetValue(hi);
            //shaderEffect.Parameters["lightRadius"].SetValue(0.15f);


            ShadowInterval shadowr = new ShadowInterval(GameWorld.Instance.BugObject.GetComponent<ShadowCaster>(), GameWorld.Instance.PlayerObject.GetComponent<LightEmitter>());
            shaderEffect.Parameters["Upper"].SetValue(shadowr.UpperAngle);
            shaderEffect.Parameters["Lower"].SetValue(shadowr.LowerAngle);
            //shaderEffect.Parameters["Offset"].SetValue(shadowr.AngleOffset);
            shaderEffect.Parameters["Distance"].SetValue(shadowr.Distance);





            (List<LightEmitter> lightEmitters, List<ShadowInterval> shadowIntervals) components = GameWorld.Instance.GetShaderData();

            GameWorld.Instance.GraphicsDevice.SetRenderTarget(LightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderEffect);
            foreach (LightEmitter light in components.lightEmitters)
            {
                Color dataPass = new Color(light.X, light.Y, light.Radius);
                spriteBatch.Draw(ShadowSprite, new Vector2(0, 0), dataPass);
            }
            //spriteBatch.Draw(ShadowSprite, new Vector2(0, 0), new Color(hi[0].X, hi[0].Y, 0.15f));
            spriteBatch.End();


            GameWorld.Instance.GraphicsDevice.SetRenderTarget(ShadowTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderEffect);
            //foreach (ShadowCaster shadow in components.shadowIntervals)
            {
                //Color dataPass = new Color(shadow.X, shadow.Y, shadow.Radius);
                //spriteBatch.Draw(ShadowSprite, new Vector2(0, 0), dataPass);
            }
            //spriteBatch.Draw(ShadowSprite, new Vector2(0, 0), new Color(hi[1].X, hi[1].Y, 0.15f));
            spriteBatch.End();

            GameWorld.Instance.GraphicsDevice.SetRenderTarget(null);
        }



        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LightTarget, Vector2.Zero, null, Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

    }
}

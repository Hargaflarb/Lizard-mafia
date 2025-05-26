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

        private Vector2 lightPosition;

        public ShadowInterval(ShadowCaster shadowCaster, LightEmitter light)
        {
            lightPosition = light.GameObject.Transform.Position;
            distance = shadowCaster.NormalizedDistanceToLight(light);
            float nondistance = shadowCaster.CalculateDistanceToLight(light);
            float BaseAngle = shadowCaster.CalculateLightToShadowAngle(light);
            float AngleIntervalSize = shadowCaster.CalculateAngle(nondistance);
            // double negativity
            angleOffset = -(BaseAngle - AngleIntervalSize);
            upperAngle = BaseAngle + AngleIntervalSize + angleOffset;
            lowerAngle = 0;
        }

        public float UpperAngle { get => upperAngle; }
        public float LowerAngle { get => lowerAngle; }
        public float AngleOffset { get => angleOffset; }
        public float Distance { get => distance; }
        public Vector2 LightPosition { get => lightPosition; set => lightPosition = value; }

        public static void ToDataPass(List<ShadowInterval> intervals, out Vector3[] shadowData, out Vector2[] lightPositions)
        {
            //float position = MathF.Floor(LightPosition.X) + (MathF.Floor(LightPosition.Y) / 1000);
            List<Vector3> data = new List<Vector3>();
            List<Vector2> positions = new List<Vector2>();

            foreach (ShadowInterval interval in intervals)
            {
                data.Add(new Vector3(interval.UpperAngle, interval.AngleOffset, interval.Distance));
                positions.Add(interval.LightPosition / GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2());
            }


            shadowData = data.ToArray();
            lightPositions = positions.ToArray();
        }
    }

    public static class ShadowMap
    {
        private static Texture2D shadowSprite;
        private static Effect shaderLightEffect;
        private static Effect shaderShadowEffect;
        private static Effect invertAplha;
        private static RenderTarget2D lightTarget;
        private static RenderTarget2D finalLightTarget;
        private static RenderTarget2D shadowTarget;
        private static Color color = Color.White;


        public static Texture2D ShadowSprite { get => shadowSprite; set => shadowSprite = value; }
        public static Effect ShaderLightEffect { get => shaderLightEffect; set => shaderLightEffect = value; }
        public static Effect ShaderShadowEffect { get => shaderShadowEffect; set => shaderShadowEffect = value; }
        public static Effect InvertAplha { get => invertAplha; set => invertAplha = value; }
        public static RenderTarget2D LightTarget { get => lightTarget; set => lightTarget = value; }
        public static RenderTarget2D FinalLightTarget { get => finalLightTarget; set => finalLightTarget = value; }
        public static RenderTarget2D ShadowTarget { get => shadowTarget; set => shadowTarget = value; }
        public static Color Color { get => color; set => color = value; }

        static ShadowMap()
        {
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            LightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            FinalLightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            ShadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
        }


        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public static void SetSprite()
        {
            ShadowSprite = GameWorld.Instance.Content.Load<Texture2D>("shadow");
            shaderLightEffect = GameWorld.Instance.Content.Load<Effect>("TestShader");
            InvertAplha = GameWorld.Instance.Content.Load<Effect>("InvertAlpha");
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
            //shaderLightEffect.Parameters["Upper"].SetValue(shadowr.UpperAngle);
            //shaderLightEffect.Parameters["Lower"].SetValue(shadowr.LowerAngle);
            //shaderLightEffect.Parameters["Offset"].SetValue(shadowr.AngleOffset);
            //shaderLightEffect.Parameters["Distance"].SetValue(shadowr.Distance);


            //shaderShadowEffect.Parameters["resolution"].SetValue(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2());

            (List<LightEmitter> lightEmitters, List<ShadowInterval> shadowIntervals) components = GameWorld.Instance.GetShaderData();
            ShadowInterval.ToDataPass(components.shadowIntervals, out Vector3[] shadowData, out Vector2[] lightPositions);
            shaderShadowEffect.Parameters["shadowData"].SetValue(shadowData);
            shaderShadowEffect.Parameters["lightPositions"].SetValue(lightPositions);



            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(ShadowTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderShadowEffect);
            int index = 0;
            foreach (ShadowInterval shadow in components.shadowIntervals)
            {
                spriteBatch.Draw(ShadowSprite, new Vector2(0,0), new Color(index++,0,0));
            }
            spriteBatch.End();


            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(LightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderLightEffect);
            foreach (LightEmitter light in components.lightEmitters)
            {
                Color dataPass = new Color(light.X, light.Y, light.Radius);
                spriteBatch.Draw(ShadowSprite, new Vector2(0, 0),null, dataPass,0,Vector2.Zero,1, SpriteEffects.None,0.4f);
            }

            spriteBatch.End();


            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(FinalLightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: InvertAplha);
            spriteBatch.Draw(LightTarget, new Vector2(0, 0), Color.White);
            spriteBatch.End();


            GameWorld.Instance.GraphicsDevice.SetRenderTarget(null);
        }



        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShadowTarget, Vector2.Zero, null, Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.4f);
            spriteBatch.Draw(FinalLightTarget, Vector2.Zero, null, Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

    }
}

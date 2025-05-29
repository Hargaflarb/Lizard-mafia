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
        private float angleOffset;
        private float distance;

        private Vector2 lightPosition;

        public ShadowInterval(ShadowCaster shadowCaster, LightEmitter light)
        {
            lightPosition = light.GameObject.Transform.Position;
            distance = light.NormalizedDistanceToLight(shadowCaster);
            float nondistance = shadowCaster.CalculateDistanceToLight(light);
            float BaseAngle = shadowCaster.CalculateLightToShadowAngle(light);
            float AngleIntervalSize = shadowCaster.CalculateAngle(nondistance);
            // double negativity

            angleOffset = -(BaseAngle - AngleIntervalSize);
            upperAngle = BaseAngle + AngleIntervalSize + angleOffset;
        }

        public float UpperAngle { get => upperAngle; }
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

        public Color ToDataPass()
        {
            float Pi = 6.28318530718f;
            return new Color(UpperAngle / (Pi), (AngleOffset / (Pi)) + 0.5f, Distance);
        }

    }

    public static class ShadowMap
    {
        private static Texture2D shadowSprite;
        private static Effect shaderLightEffect;
        private static Effect invertAplha;
        private static RenderTarget2D lightTarget;
        private static RenderTarget2D finalLightTarget;
        private static Color color = Color.White;


        public static Texture2D ShadowSprite { get => shadowSprite; set => shadowSprite = value; }
        public static Effect ShaderLightEffect { get => shaderLightEffect; set => shaderLightEffect = value; }
        public static Effect InvertAplha { get => invertAplha; set => invertAplha = value; }
        public static RenderTarget2D LightTarget { get => lightTarget; set => lightTarget = value; }
        public static RenderTarget2D FinalLightTarget { get => finalLightTarget; set => finalLightTarget = value; }
        public static Color Color { get => color; set => color = value; }

        static ShadowMap()
        {
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            LightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            FinalLightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
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
            //ShaderShadowEffect = GameWorld.Instance.Content.Load<Effect>("TestShaderShadow");
            //ShadowMapSprite = GameWorld.Instance.Content.Load<Texture2D>("shadowMap");
        }

        public static void PrepareShadows(SpriteBatch spriteBatch)
        {
            List<(LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals)> components = GameWorld.Instance.GetShaderData();

            //-------------

            foreach ((LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals) shadow in components)
            {
                shadow.lightEmitter.DrawShadowsToTarget(spriteBatch, shadow.shadowIntervals);
            }

            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(LightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderLightEffect);
            foreach ((LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals) shadow in components)
            {
                shadow.lightEmitter.DrawToLightMask(spriteBatch);
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
            spriteBatch.Draw(FinalLightTarget, Vector2.Zero, null, Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }

    }
}

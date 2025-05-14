using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class ShadowMap : Component
    {
        private RenderTarget2D shadowTarget;
        private Texture2D shadowSprite;
        private Effect shaderEffect;
        private Vector2 origin;
        private Color color = Color.White;

        public RenderTarget2D ShadowTarget { get => shadowTarget; set => shadowTarget = value; }
        public Texture2D ShadowSprite { get => shadowSprite; set => shadowSprite = value; }
        public Effect ShaderEffect { get => shaderEffect; set => shaderEffect = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Color Color { get => color; set => color = value; }

        public ShadowMap(GameObject gameObject) : base(gameObject)
        {
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            ShadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
        }


        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public void SetSprite()
        {
            ShadowSprite = GameWorld.Instance.Content.Load<Texture2D>("shadow");
            shaderEffect = GameWorld.Instance.Content.Load<Effect>("TestShader");
            //ShadowMapSprite = GameWorld.Instance.Content.Load<Texture2D>("shadowMap");
        }

        public override void Start()
        {

        }

        public void PrepareShadows(SpriteBatch spriteBatch)
        {
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(ShadowTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));


            Vector2[] hi = new Vector2[] { GameWorld.Instance.PlayerObject.Transform.Position / GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2(), new Vector2(0.4f, 0.4f), Vector2.One * 2, Vector2.One * 2, new Vector2(0.1f, 0.7f) };
            float[] bye = new float[hi.Length * 2];
            for (int i = 0; i < hi.Length; i++)
            {
                bye[i * 2] = hi[i].X;
                bye[i * 2 + 1] = hi[i].Y;
            }
            shaderEffect.Parameters["lightPositions"].SetValue(hi);
            shaderEffect.Parameters["lightRadius"].SetValue(0.15f);

            spriteBatch.Begin(blendState: BlendState.Additive, effect: shaderEffect);
            spriteBatch.Draw(ShadowSprite, new Vector2(0, 0), color);
            spriteBatch.End();

            GameWorld.Instance.GraphicsDevice.SetRenderTarget(null);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shadowTarget, GameObject.Transform.Position, null, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, 1);
        }

    }
}

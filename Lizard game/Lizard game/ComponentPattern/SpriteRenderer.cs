using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class SpriteRenderer : Component
    {
        private Texture2D sprite;
        private Vector2 origin;
        private Color color = Color.White;
        private bool doSizeRender = false;

        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Color Color { get => color; set => color = value; }
        public bool DoSizeRender { get => doSizeRender; set => doSizeRender = value; }

        public float ScaledWidth
        {
            get => Sprite.Width * GameObject.Transform.Scale;
        }
        public float ScaledHeight
        {
            get => Sprite.Height * GameObject.Transform.Scale;
        }

        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
        }

        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }
        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public void SetSprite(Texture2D sprite)
        {
            Sprite = sprite;
        }

        public override void Start()
        {
            SetOrigin();
        }

        public void SetOrigin()
        {
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!doSizeRender)
            {
                spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(Sprite, new Rectangle((int)(GameObject.Transform.Position.X - GameObject.Transform.Size.X / 2f), (int)(GameObject.Transform.Position.Y - GameObject.Transform.Size.Y / 2f), (int)GameObject.Transform.Size.X, (int)GameObject.Transform.Size.Y), Color);
            }
        }
    }
}

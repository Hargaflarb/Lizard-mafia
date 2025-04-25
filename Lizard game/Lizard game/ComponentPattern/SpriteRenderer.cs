using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class SpriteRenderer : Component
    {
        private Texture2D sprite;
        private Vector2 origin;
        private Color color = Color.White;
        private SpriteBatch spriteBatch;
        private bool drawingLine=false;
        private float distance;
        private float angle;
        private Texture2D lineTexture;
        private Vector2 lineOrigin;
        private Vector2 scale;
        private Vector2 linestart;

        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Color Color { get => color; set => color = value; }
        
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
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        public void Trasparancy(int value)
        {
            if (color.A != value)
            {
                color = new Color(color.R, color.G, color.B, value);
            }
        }

        public void DrawLine(Texture2D lineTexture,Vector2 point1,Vector2 point2,float thickness=5f)
        {
            distance = Vector2.Distance(point1, point2);
            angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            lineOrigin = new Vector2(0f, 0.5f);
            scale = new Vector2(distance, thickness);
            this.lineTexture = lineTexture;
            linestart = point1;
            drawingLine = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, 0);
            if (drawingLine)
            {
                spriteBatch.Draw(lineTexture, linestart, null, Color.White, angle, lineOrigin, scale, SpriteEffects.None, 0);
            }
            //store spritebatch for later
            if (this.spriteBatch == null)
            {
                this.spriteBatch = spriteBatch;
            }
        }
    }
}

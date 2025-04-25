using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Tongue : Component
    {
        private Texture2D tongueTexture;
        private bool inUse;

        public Tongue(GameObject gameObject) : base(gameObject)
        {
        }
        public void AddTexture(SpriteBatch spriteBatch)
        {
            if (tongueTexture == null)
            {
                tongueTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                tongueTexture.SetData(new[] { Color.Pink });
            }
        }

        public void Use()
        {
            //get player position
            Vector2 point1 = new Vector2(GameObject.Transform.Position.X + ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).Sprite.Width / 2, GameObject.Transform.Position.Y);
            //get mouse position
            MouseState mouseState = Mouse.GetState();
            Vector2 point2 = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            //get distance & angle
            ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).DrawLine(tongueTexture, point1, point2);
        }
    }
}

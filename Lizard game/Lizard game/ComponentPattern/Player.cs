using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Player : Component
    {
        private float speed;

        public Player(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2,
                GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3 - 200);
            speed = 300;
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);
        }

        public override void Update()
        {

        }
    }
}

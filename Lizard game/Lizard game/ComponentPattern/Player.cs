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
        public const float walkingSpeed = 100;
        public const float runningSpeed = 300;
        public const float jumpSpeed = 40;

        private float speed;
        private bool isHiding;

        public float Speed
        {
            get => speed;
            set
            {
                if (value < walkingSpeed)
                {
                    speed = 0;
                    Velocity = Vector2.Zero;
                }
                speed = value;
            }
        }
        public Vector2 Velocity { get => GameObject.Velocity; set => GameObject.Velocity = value; }
        public bool IsHiding { get => isHiding; set => isHiding = value; }

        public Player(GameObject gameObject) : base(gameObject)
        {
            Speed = 0;
            Velocity = Vector2.Zero;
            IsHiding = false;

        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2,
                GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3 - 200);

            Speed = 300;
            GameObject.Transform.Scale = 0.2f;
            

        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            velocity *= Speed;
            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);
        }

        public void Jump()
        {

        }


        public override void Update()
        {
            Move(Velocity);
            Speed *= 0.98f;
            if (Speed == 0)
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Idle");
            }
        }

        public override void OnCollision(Collider collider)
        {
            base.OnCollision(collider);
            Speed = 0;
        }
    }
}

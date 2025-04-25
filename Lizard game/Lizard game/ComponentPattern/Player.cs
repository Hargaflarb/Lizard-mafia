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
        public const float jumpSpeed = 400;

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
                    XVelocity = 0;
                }
                speed = value;
            }
        }
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

        public void Move()
        {
            float xVelocity = 0;
            if (XVelocity != 0)
            {
                xVelocity = (XVelocity < 0 ? -1 : 1);
            }

            xVelocity *= Speed;

            Vector2 velocity = new Vector2(xVelocity, YVelocity);
            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);
        }

        public void Jump()
        {
            if ((bool)((Gravity)GameObject.GetComponent<Gravity>())?.TouchingGround)
            {
                YVelocity = -jumpSpeed;
            }
        }


        public override void Update()
        {
            Move();
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

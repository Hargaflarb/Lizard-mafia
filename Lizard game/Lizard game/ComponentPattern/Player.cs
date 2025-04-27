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
        public const float jumpSpeed = 250;

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

        public void Sprint()
        {
            if ((bool)((Gravity)GameObject.GetComponent<Gravity>())?.TouchingGround)
            {
                Speed *= 1.04f;
                if (Speed > runningSpeed)
                {
                    Speed = runningSpeed;
                }
            }
        }

        public override void Update()
        {
            Move();

            if ((bool)((Gravity)GameObject.GetComponent<Gravity>())?.TouchingGround)
            {
                Speed *= 0.98f;
            }

            if (Speed == 0)
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Idle");
            }
        }

        public override void OnCollision(Collider collider)
        {
            base.OnCollision(collider);
        }
    }
}

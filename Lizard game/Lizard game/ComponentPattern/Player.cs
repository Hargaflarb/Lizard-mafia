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
        public const float runningSpeed = 400;
        public const float jumpSpeed = 300;

        private float speed;
        private bool isHiding;
        private bool isWalking;
        private int health;
        private float invincibility;

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
            health = 2;
            Speed = 0;
            Velocity = Vector2.Zero;
            IsHiding = false;
            isWalking = false;
        }

        public override void Start()
        {
            GameObject.Transform.Scale = 0.2f;
        }

        public override void Update()
        {
            Move();

            if ((bool)GameObject.GetComponent<Gravity>()?.TouchingGround)
            {
                Speed -= runningSpeed * 0.01f;
            }

            if (Speed == 0)
            {
                SetPlayerAnimation("Idle");
            }
        }

        public override void OnCollision(Collider collider)
        {
            base.OnCollision(collider);
        }

        public void SetPlayerAnimation(string animationName)
        {
            GameWorld.Instance.PlayerObject.GetComponent<Animator>().PlayAnimation(animationName);
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
            if ((bool)GameObject.GetComponent<Gravity>()?.TouchingGround)
            {
                YVelocity = -jumpSpeed;
            }
        }

        public void Walk(int horisontalVelocity)
        {
            isWalking = true;
            if (Speed < walkingSpeed)
            {
                Speed = walkingSpeed;
            }

            //if both are either positiv or negativ
            if ((XVelocity <= 0 & horisontalVelocity <= 0) | (XVelocity >= 0 & horisontalVelocity >= 0))
            {
                XVelocity = horisontalVelocity;
            }
            else
            {
                Speed -= runningSpeed * 0.01f;
            }


            if (Speed <= walkingSpeed)
            {
                SetPlayerAnimation("Walk");
            }
            invincibility -= (float)GameWorld.Instance.DeltaTime;
        }

        public void Sprint()
        {
            if ((bool)GameObject.GetComponent<Gravity>()?.TouchingGround & isWalking)
            {
                Speed += runningSpeed * 0.04f;
                if (Speed > runningSpeed)
                {
                    Speed = runningSpeed;
                }
            }
        }

        public void TakeDamage()
        {
            if (invincibility <= 0)
            {
                health--;
                invincibility = 5;
            }
            if (health <= 0)
            {
                GameWorld.Instance.RemoveObject(GameWorld.Instance.PlayerObject);
                GameWorld.Instance.IsAlive = false;
            }
        }
    }
}

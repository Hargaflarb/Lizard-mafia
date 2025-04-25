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
                    Velocity = Vector2.Zero;
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

        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

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
            invincibility -= (float)GameWorld.Instance.DeltaTime;
        }

        public override void OnCollision(Collider collider)
        {
            base.OnCollision(collider);
            Speed = 0;
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

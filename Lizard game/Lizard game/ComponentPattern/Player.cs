using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Vector2 velocity;
        private bool isHiding;

        public float Speed
        {
            get => speed;
            set
            {
                if (value < walkingSpeed)
                {
                    speed = 0;
                    velocity = Vector2.Zero;
                }
                speed = value;
            }
        }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public bool IsHiding { get => isHiding; set => isHiding = value; }

        public Player(GameObject gameObject) : base(gameObject)
        {
            speed = 0;
            velocity = Vector2.Zero;
            isHiding = false;

        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2,
                GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3 - 200);

            Speed = 300;

        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            velocity *= Speed;
            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);
            //change animation based on movement
            if (velocity != Vector2.Zero && speed < runningSpeed)
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Walk");
            }
            else if (velocity == Vector2.Zero) 
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Idle");
            }
        }

        public void Jump()
        {

        }


        public override void Update()
        {
            if (IsHiding)
            {
                ((SpriteRenderer)GameWorld.Instance.PlayerObject.GetComponent<SpriteRenderer>()).Trasparancy(50);
            }
            else
            {
                ((SpriteRenderer)GameWorld.Instance.PlayerObject.GetComponent<SpriteRenderer>()).Trasparancy(255);
                Move(velocity);
            }
            Speed *= 0.98f;
        }
    }
}

using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

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
        private Texture2D tongueTexture;

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

        public void AddTexture(SpriteBatch spriteBatch)
        {
            if (tongueTexture == null)
            {
                tongueTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                tongueTexture.SetData(new[] { Color.Pink });
            }
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
                ((Animator)GameObject.GetComponent<Animator>()).PlayAnimation("Walk");
            }
            else if (velocity == Vector2.Zero)
            {
                ((Animator)GameObject.GetComponent<Animator>()).PlayAnimation("Idle");
            }
        }

        public void Jump()
        {

        }

        public void Tongue()
        {
            //get player position
            Vector2 point1 = new Vector2(GameWorld.Instance.PlayerObject.Transform.Position.X + ((SpriteRenderer)GameWorld.Instance.PlayerObject.GetComponent<SpriteRenderer>()).Sprite.Width / 2, GameWorld.Instance.PlayerObject.Transform.Position.Y);
            //get mouse position
            MouseState mouseState = Mouse.GetState();
            Vector2 point2 = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            //get distance & angle
            ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).DrawLine(tongueTexture, point1, point2);
        }

        public override void Update()
        {
            //makes the palyer seethrough & unable to move when hiding
            if (IsHiding)
            {
                ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).Trasparancy(50);
            }
            else
            {
                ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).Trasparancy(255);
                Move(velocity);
            }
            Speed *= 0.98f;
        }
    }
}

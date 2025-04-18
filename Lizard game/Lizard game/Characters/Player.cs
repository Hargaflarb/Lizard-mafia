﻿using Lizard_game.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Characters
{
    public class Player : Component
    {
        public const float walkingSpeed = 1;
        public const float runningSpeed = 3;
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
            sr.SetSprite("");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, 
                (GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3) - 200);
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
        }

        public void Jump()
        {

        }

        public override void Update()
        {
            
        }
    }
}

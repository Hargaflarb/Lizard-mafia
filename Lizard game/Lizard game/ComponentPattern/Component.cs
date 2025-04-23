using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public abstract class Component
    {
        private GameObject gameObject;
        public GameObject GameObject { get; private set; }

        public Component(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnCollision(Collider collider)
        {
            if (collider.gameObject.GetComponent<Wall>() is not null)
            {
                Vector2 difference = gameObject.Transform.Position - collider.gameObject.Transform.Position;
                Collider thisCollider = (Collider)gameObject.GetComponent<Collider>();
                float newX;
                float newY;
                //true if y value should be changed
                if (Math.Abs(difference.X) < Math.Abs(difference.Y))
                {
                    float targetDif = collider.CollisionBox.Height / 2 + thisCollider.CollisionBox.Height / 2;
                    //sets a new Y, based on wether it colliding from above or bellow.
                    newY = collider.gameObject.Transform.Position.Y + (difference.Y < 0 ? -targetDif : targetDif);
                    newX = GameObject.Transform.Position.X;
                }
                else //if x value should be changed
                {
                    float targetDif = collider.CollisionBox.Width / 2 + thisCollider.CollisionBox.Width / 2;
                    //sets a new X, based on wether it colliding from the right or left.
                    newX = collider.gameObject.Transform.Position.X + (difference.X < 0 ? -targetDif : targetDif);
                    newY = GameObject.Transform.Position.Y;
                }

                gameObject.Transform.Position = new Vector2(newX, newY);
            }
        }

    }
}

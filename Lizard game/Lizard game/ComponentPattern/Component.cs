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
        public GameObject GameObject { get => gameObject; private set => gameObject = value; }
        public Vector2 Velocity { get => GameObject.Velocity; set => GameObject.Velocity = value; }
        public float XVelocity { get => GameObject.XVelocity; set => GameObject.XVelocity = value; }
        public float YVelocity { get => GameObject.YVelocity; set => GameObject.YVelocity = value; }

        public Component(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnCollision(Collider collider) { }

    }
}

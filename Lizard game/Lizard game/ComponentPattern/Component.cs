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
        public virtual void OnCollision() { }

    }
}

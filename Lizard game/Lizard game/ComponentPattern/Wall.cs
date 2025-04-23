using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Wall : Component
    {
        public Wall(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Awake()
        {
            base.Awake();
        }
        public override void Start()
        {
            ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).SetSprite("butan");
            base.Start();
        }
        public override void Update()
        {
            base.Update();
        }

    }
}

using Microsoft.Xna.Framework;
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
        public Wall(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            gameObject.Transform.Position = position;
        }

        public override void Awake()
        {
            ((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).SetSprite(GameWorld.Instance.Content.Load<Texture2D>("butan"));
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update()
        {
            base.Update();
        }

    }
}

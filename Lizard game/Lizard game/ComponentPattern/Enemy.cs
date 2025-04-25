using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinding;

namespace Lizard_game.ComponentPattern
{
    public class Enemy : Component
    {
        float speed;

        
        

        public Enemy(GameObject gameObject) : base(gameObject)
        {
        }


        public void Move(Vector2 velocity)
        {

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            GameObject.Transform.Translate(velocity * speed * GameWorld.Instance.DeltaTime);
        }
    }
}

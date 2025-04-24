using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Enemy : Component
    {
        public Enemy(GameObject gameObject) : base(gameObject)
        {
        }

        public void Move(Vector2 velocity)
        {

        }
    }
}

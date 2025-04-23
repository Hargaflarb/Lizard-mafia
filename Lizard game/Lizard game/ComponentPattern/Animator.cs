using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Animator : Component
    {
        private int currentIndex;
        private float elapsedTime;
        private SpriteRenderer spriteRenderer;
        

        public Animator(GameObject gameObject) : base(gameObject)
        {
        }
    }
}

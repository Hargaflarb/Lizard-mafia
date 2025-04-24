using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Gravity : Component
    {
        private const float gravitation = 50;
        private bool touching;
        private float verticalVelocity;

        public Gravity(GameObject gameObject) : base(gameObject)
        {

        }


        public override void Update()
        {
            if (!touching)
            {
                verticalVelocity += gravitation;
            }
            else
            {
                verticalVelocity = 0;
            }
        }
    }
}

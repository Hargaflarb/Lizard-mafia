using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Lizard_game.ComponentPattern
{
    public class Gravity : Component
    {
        private const float gravitation = 50;
        private bool touching = false;
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
                touching = false;
            }
            //GameObject.Velocity += new Vector2(0, verticalVelocity);
        }

        public override void OnCollision(Collider collider)
        {
            if (collider.GameObject.GetComponent<Wall>() is not null)
            {
                Vector2 difference = GameObject.Transform.Position - collider.GameObject.Transform.Position;

                //true if y value should be changed
                if (Math.Abs(difference.X) < Math.Abs(difference.Y))
                {
                    touching = true;
                }

            }

            base.OnCollision(collider);
        }
    }
}

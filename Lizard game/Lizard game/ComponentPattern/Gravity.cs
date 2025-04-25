using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Lizard_game.ComponentPattern
{
    public class Gravity : Component
    {
        private const float gravitation = 5;
        private bool touching = false;
        private Collider collider;

        public bool TouchingGround { get => touching; set => touching = value; }

        public Gravity(GameObject gameObject) : base(gameObject)
        {

        }


        public override void Update()
        {
            if (!TouchingGround)
            {
                GameObject.YVelocity += gravitation;
            }
            else if (!collider.IsTouching((Collider)GameObject.GetComponent<Collider>()))
            {
                TouchingGround = false;
            }
        }

        public override void OnCollision(Collider collider)
        {
            if (collider.GameObject.GetComponent<Wall>() is not null)
            {
                Vector2 difference = GameObject.Transform.Position - collider.GameObject.Transform.Position;

                //true if y value should be changed
                if (Math.Abs(difference.X) < Math.Abs(difference.Y))
                {
                    if (difference.Y < 0)
                    {
                        TouchingGround = true;
                        this.collider = collider;
                    }
                }

            }

            base.OnCollision(collider);
        }


    }
}

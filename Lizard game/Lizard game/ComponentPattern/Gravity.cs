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
            else if (!((Collider)GameObject.GetComponent<Collider>()).IsTouchingTopOf(collider))
            {
                TouchingGround = false;
            }
        }

        public override void OnCollision(Collider collider)
        {
            if (collider.GameObject.GetComponent<Wall>() is not null)
            {
                Collider thisCollider = (Collider)GameObject.GetComponent<Collider>();
                
                //true if y value should be changed
                if (thisCollider.IsTouchingTopOf(collider))
                {
                    TouchingGround = true;
                    this.collider = collider;
                }

            }
        }


    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinding;
using Lizard_game.StatePatterns;

namespace Lizard_game.ComponentPattern
{
    public class Enemy : Component
    {
        float speed = 300;
        float timeSinceCollision;
        private IState<Enemy> currentState;

        
        

        public Enemy(GameObject gameObject) : base(gameObject)
        {
            currentState = new MoveState();
            ChangeState(currentState);
        }


        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            GameObject.Transform.Translate(velocity * speed * GameWorld.Instance.DeltaTime);
        }

        public override void Update()
        {
            currentState.Execute();
            timeSinceCollision += (float)GameWorld.Instance.DeltaTime;
            if (timeSinceCollision >= 5)
            {
                speed = 300;
                
            }
        }

        /// <summary>
        /// Is mostly here to allow changing states, but as of writing there's only one state. May add more
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(IState<Enemy> state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = state;
            currentState.Enter(this);
        }

        public override void OnCollision(Collider collider)
        {
            timeSinceCollision = 0;            
            base.OnCollision(collider);
            Player collidingObject = collider.GameObject.GetComponent<Player>() as Player;
            
           
            collidingObject.TakeDamage();
            speed = 0;
        }
    }
}

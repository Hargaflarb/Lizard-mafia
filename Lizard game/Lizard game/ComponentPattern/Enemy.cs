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
        }

        public void ChangeState(IState<Enemy> state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = state;
            currentState.Enter(this);
        }
    }
}

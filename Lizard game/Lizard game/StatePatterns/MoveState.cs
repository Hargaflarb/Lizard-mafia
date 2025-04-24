using Lizard_game.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.StatePatterns
{
    public class MoveState : IState<Enemy>
    {
        private Enemy parent;
        private Player player;

        

        public void Enter(Enemy parent)
        {
            this.parent = parent;
            player = (Player)GameWorld.Instance.PlayerObject.GetComponent<Player>();
        }

        public void Execute()
        {
            parent.Move(parent.Velocity);
            Vector2 playerPos = player.GameObject.Transform.Position;
            Vector2 enemyPos = player.GameObject.Transform.Position;
        }

        public void Exit()
        {
            
        }
    }
}

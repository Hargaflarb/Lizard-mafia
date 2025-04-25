using Lizard_game.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinding;

namespace Lizard_game.StatePatterns
{
    public class MoveState : IState<Enemy>
    {
        private Enemy parent;
        private Player player;
        private bool foundPlayer;
        
        

        

        public void Enter(Enemy parent)
        {
            this.parent = parent;
            player = (Player)GameWorld.Instance.PlayerObject.GetComponent<Player>();
        }

        public void Execute()
        {
            parent.Move(parent.Velocity);
            Vector2 playerPos = player.GameObject.Transform.Position;
            Vector2 enemyPos = parent.GameObject.Transform.Position;

            GameWorld.Instance.Graph.AStar(((int)enemyPos.X, (int)enemyPos.Y), ((int)playerPos.X, (int)playerPos.Y), out foundPlayer);
        }

        public void Exit()
        {
            
        }
    }
}

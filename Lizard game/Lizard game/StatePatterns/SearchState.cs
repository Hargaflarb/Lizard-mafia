using Lizard_game.ComponentPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Lizard_game.StatePatterns
{
    public class SearchState : IState<Enemy>
    {
        public const int discoveryDisatnace = 200;
        private Enemy parent;
        private Player player;

        public void Enter(Enemy parent)
        {
            this.parent = parent;
            player = GameWorld.Instance.PlayerObject.GetComponent<Player>();
        }

        public void Execute()
        {
            Vector2 playerPos = player.GameObject.Transform.Position;
            Vector2 enemyPos = parent.GameObject.Transform.Position;
            Vector2 direction = playerPos - enemyPos;

            if (direction.Length() <= discoveryDisatnace)
            {
                parent.ChangeState(new MoveState());
            }
        }

        public void Exit()
        {

        }
    }
}

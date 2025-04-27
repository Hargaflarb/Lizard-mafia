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
        public const int trackingDisatnace = 700;
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

            if (direction.Length() <= trackingDisatnace)
            {
                direction.Normalize();
                parent.Move(direction);
            }
            else
            {
                parent.ChangeState(new SearchState());
            }
        }

        public void Exit()
        {

        }
    }
}

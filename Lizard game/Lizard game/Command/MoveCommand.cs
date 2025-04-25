using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.ComponentPattern;


namespace Lizard_game.Command
{
    public class MoveCommand : ICommand
    {
        private Player player;
        private float horisontalVelocity;

        public MoveCommand(Player player, float velocity)
        {
            this.player = player;
            horisontalVelocity = velocity;
        }

        public void Execute()
        {
            if (player.Speed < Player.walkingSpeed)
            {
                player.Speed = Player.walkingSpeed;
                player.XVelocity = horisontalVelocity;
            }
            if (player.Speed <= Player.walkingSpeed)
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Walk");
            }
        }
    }
}

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
        private Vector2 velocity;

        public MoveCommand(Player player, Vector2 velocity)
        {
            this.player = player;
            this.velocity = velocity;
        }

        public void Execute()
        {
            if (player.Speed < Player.walkingSpeed)
            {
                player.Speed = Player.walkingSpeed;
                player.Velocity = velocity;
            }
            
            if (player.Speed <= Player.walkingSpeed)
            {
                ((Animator)GameWorld.Instance.PlayerObject.GetComponent<Animator>()).PlayAnimation("Walk");
            }
        }
    }
}

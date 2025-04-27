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
        private int horisontalVelocity;

        public MoveCommand(Player player, int velocity)
        {
            this.player = player;
            horisontalVelocity = velocity;
        }

        public void Execute()
        {
            player.Walk(horisontalVelocity);
        }
    }
}

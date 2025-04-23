using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.ComponentPattern;


namespace Lizard_game.Command
{
    public class SprintCommand : ICommand
    {
        private Player player;

        public SprintCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Speed *= 1.2f;
            if (player.Speed > Player.runningSpeed)
            {
                player.Speed = Player.runningSpeed;
            }
        }
    }
}

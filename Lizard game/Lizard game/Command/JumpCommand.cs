using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.Characters;
using Lizard_game.ComponentPattern;


namespace Lizard_game.Command
{
    public class JumpCommand : ICommand
    {
        private Player player;

        public JumpCommand(Player player)
        {
            this.player = player;
        }


        public void Execute()
        {
            player.Jump();
        }
    }
}

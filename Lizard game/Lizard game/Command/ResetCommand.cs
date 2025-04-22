using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.ComponentPattern;

namespace Lizard_game.Command
{
    public class ResetCommand : ICommand
    {
        private Player player;

        public ResetCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {

        }
    }
}

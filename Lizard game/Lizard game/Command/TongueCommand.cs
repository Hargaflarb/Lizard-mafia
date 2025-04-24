using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.ComponentPattern;


namespace Lizard_game.Command
{
    /// <summary>
    /// Descripes the state for a players tongue.
    /// </summary>
    public enum TongueState
    {
        Extending = 0,
        Extended = 1,
        Retracting = 2,
        Retracted = 3,
    }


    public class TongueCommand : ICommand
    {
        private TongueState tongueState;
        private Player player;

        public TongueCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Tongue();
        }
    }
}

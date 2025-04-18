using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TongueState tongueState;


        public void Execute()
        {

        }
    }
}

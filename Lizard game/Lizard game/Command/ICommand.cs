using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Command
{
    public interface ICommand
    {
        /// <summary>
        /// Executes a command
        /// </summary>
        void Execute();
    }
}

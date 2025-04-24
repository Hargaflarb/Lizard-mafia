using Lizard_game.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.StatePatterns
{
    public interface IState<T>
    {
        void Enter(T parent);
        void Execute();
        void Exit();
    }
}

using Lizard_game.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Factory
{
    public abstract class Factory
    {
        public abstract GameObject Create();
    }
}

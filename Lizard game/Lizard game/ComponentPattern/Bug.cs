using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Bug : Component
    {

        public Bug(GameObject gameObject) : base(gameObject)
        {
            
        }

        public override void Start()
        {
            base.Start();
        }

    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class LightEmitter : Component
    {
        public LightEmitter(GameObject gameObject) : base(gameObject)
        {

        }


        public Vector2 AlteredPosition
        {
            get
            {
                return GameObject.Transform.Position / GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2();
            }
        }
    }
}

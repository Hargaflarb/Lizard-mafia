using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class LightEmitter : Component
    {
        private float radius;
        public float Radius { get => radius; set => radius = value; }
        public float X { get => GameObject.Transform.Position.X / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth; }
        public float Y { get => GameObject.Transform.Position.Y / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight; }
        
        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            this.Radius = radius;
        }
    }
}

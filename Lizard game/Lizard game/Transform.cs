using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game
{
    public class Transform
    {
        private Vector2 position;
        private float rotation;
        private float scale;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public void Translate(Vector2 translation)
        {
            Position += translation;
        }

        public Transform()
        {
            Scale = 1;
        }
    }
}

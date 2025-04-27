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
        private Vector2 size;
        private float rotation;
        private float scale;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Size { get => size; set => size = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }

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

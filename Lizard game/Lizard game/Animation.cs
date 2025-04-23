using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game
{
    public class Animation
    {
        #region Fields
        private float fps;
        private string name;
        private Texture2D[] sprites;

        #endregion
        #region Properties
        public string Name { get => name; set => name = value; }
        public float Fps { get => fps; set => fps = value; }
        public Texture2D[] Sprites { get => sprites; set => sprites = value; }
        #endregion
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the animation</param>
        /// <param name="sprites">The sprites for the animation</param>
        /// <param name="fps">Frames per second</param>
        public Animation(string name, Texture2D[] sprites,float fps)
        {
            this.name = name;
            this.sprites = sprites;
            this.fps = fps;
        }
        #endregion
        #region Methods
        public Texture2D GetAnimationSprite(int index)
        {
            return sprites[index];
        }
        /*
        public void LoadSprites()
        {

        }*/
        #endregion

    }
}

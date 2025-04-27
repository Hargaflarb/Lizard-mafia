using Lizard_game.ComponentPattern;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Factory
{
    internal class WallFactory : Factory
    {
        private static WallFactory instance;

        public static WallFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WallFactory();
                }
                return instance;
            }
        }

        public override GameObject Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// constructs a wall.
        /// </summary>
        /// <param name="position">position of the walls upper left corner.</param>
        /// <param name="size">the size of the wall.</param>
        /// <returns></returns>
        public GameObject CreateWall(Vector2 position, Vector2 size)
        {
            GameObject wallObject = new GameObject();
            SpriteRenderer wallSpriteRenderer = wallObject.AddComponent<SpriteRenderer>();
            wallSpriteRenderer.DoSizeRender = true;
            wallSpriteRenderer.SetSprite("mincwaft_gwast");
            wallObject.AddComponent<Collider>();
            wallObject.AddComponent<Wall>(position, size);
            return wallObject;
        }

        /// <summary>
        /// constructs a wall.
        /// </summary>
        /// <param name="rectangle">The rectangle of space that it fills.</param>
        /// <returns></returns>
        public GameObject CreateWall(Rectangle rectangle)
        {
            return CreateWall(new Vector2(rectangle.X,rectangle.Y), new Vector2(rectangle.Width,rectangle.Height));
        }

    }

}

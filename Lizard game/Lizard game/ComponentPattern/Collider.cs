using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizard_game.ComponentPattern
{
    public class Collider : Component
    {
        private Texture2D pixel;
        private SpriteRenderer spriteRenderer;
        private bool isDrawing;
        private List<RectangleData> pixelPerfectRectangles;

        public List<RectangleData> PixelPerfectRectangles { get => pixelPerfectRectangles; set => pixelPerfectRectangles = value; }

        public Collider(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            pixel = GameWorld.Instance.Pixel;
            PixelPerfectRectangles=new List<RectangleData>();
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2,
                    (int)GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2,
                    spriteRenderer.Sprite.Width,
                    spriteRenderer.Sprite.Height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isDrawing)
            {
                DrawRectangle(CollisionBox, spriteBatch);
                foreach (var rect in PixelPerfectRectangles)
                {
                    DrawRectangle(rect.Rectangle, spriteBatch);
                }
            }
        }

        public override void Update()
        {
            ///UpdatePixelCollider();
        }

        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(pixel, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void UpdatePixelCollider()
        {
            for (int i = 0; i < PixelPerfectRectangles.Count; i++)
            {
                PixelPerfectRectangles[i].UpdatePosition(GameObject, spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);
            }
        }

        public void ToggleDrawing(bool isDrawing)
        {
            this.isDrawing = isDrawing;
        }

        public List<RectangleData> CreateRectangles()
        {
            List<Color[]> lines = new List<Color[]>();

            for (int y = 0; y < spriteRenderer.Sprite.Height; y++)
            {
                Color[] colors = new Color[spriteRenderer.Sprite.Width];
                spriteRenderer.Sprite.GetData(0, new Rectangle(0, y, spriteRenderer.Sprite.Width, 1), colors, 0, spriteRenderer.Sprite.Width);
                lines.Add(colors);
            }
            List<RectangleData> returnListOfRectangles = new List<RectangleData>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x].A != 0)
                    {
                        if ((x == 0)
                            || (x == lines[y].Length)
                            || (x > 0 && lines[y][x - 1].A == 0)
                            || (y == 0)
                            || (y > 0 && lines[y - 1][x].A == 0)
                            || (y < lines.Count - 1 && lines[y + 1][x].A == 0))
                        {
                            RectangleData rd = new RectangleData(x, y);

                            returnListOfRectangles.Add(rd);
                        }
                    }
                }
            }
            return returnListOfRectangles;
        }


        /// <summary>
        /// checks if the two colliders are colliding acording to pixel perfect collision
        /// </summary>
        /// <param name="colider">other collider</param>
        /// <returns>wether or not the two are colliding</returns>
        public bool IsPixelPerfectColliding(Collider colider)
        {
            if (CollisionBox.Intersects(colider.CollisionBox))
            {
                foreach (RectangleData data1 in PixelPerfectRectangles)
                {
                    foreach (RectangleData data2 in colider.PixelPerfectRectangles)
                    {
                        if (data1.Rectangle.Intersects(data2.Rectangle))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        /// <summary>
        /// checks if the two colliders are colliding.
        /// </summary>
        /// <param name="colider">other collider</param>
        /// <returns>wether or not the two are colliding</returns>
        public bool IsColliding(Collider colider)
        {
            return CollisionBox.Intersects(colider.CollisionBox);
        }

    }

    public class RectangleData
    {
        private Rectangle rectangle;
        private int x;
        private int y;

        public Rectangle Rectangle { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public RectangleData(int x, int y)
        {
            this.Rectangle = new Rectangle();
            this.X = x;
            this.Y = y;
        }
        
        public void UpdatePosition(GameObject gameObject, int width, int height)
        {
            Rectangle = new Rectangle((int)gameObject.Transform.Position.X + X - width / 2,
                (int)gameObject.Transform.Position.Y + Y - height / 2, 1, 1);
        }
    }
}

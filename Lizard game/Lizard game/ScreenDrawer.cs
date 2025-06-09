using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lizard_game.Command;
using Lizard_game.ComponentPattern;
using Lizard_game.Factory;


namespace Lizard_game
{
    public static class ScreenDrawer
    {
        private static RenderTarget2D renderTarget;
        private static GraphicsDevice graphics;
        private static Effect blurEffect;
        

        static ScreenDrawer()
        {
            graphics = GameWorld.Instance.GraphicsDevice;
            renderTarget = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
        }

        public static void LoadContent()
        {
            blurEffect = GameWorld.Instance.Content.Load<Effect>("BlurEffect");
        }

        public static RenderTarget2D ApplyEffectTo(SpriteBatch spriteBatch, RenderTarget2D image)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Black);

            blurEffect.Parameters["resolution"].SetValue(graphics.PresentationParameters.Bounds.Size.ToVector2());

            spriteBatch.Begin(effect: blurEffect);
            spriteBatch.Draw(image, Vector2.Zero, Color.White);
            spriteBatch.End();

            return renderTarget;
        }
    }
}

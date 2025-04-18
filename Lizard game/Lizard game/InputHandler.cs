using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lizard_game.Command;

namespace Lizard_game
{
    public static class InputHandler
    {
        private static Dictionary<Keys, ICommand> heldKeyBinds;
        private static Dictionary<Keys, ICommand> clickedKeyBinds;
        private static KeyboardState lastKeyboardState;

        static InputHandler()
        {
            heldKeyBinds = new Dictionary<Keys, ICommand>();
            clickedKeyBinds = new Dictionary<Keys, ICommand>();
            lastKeyboardState = new KeyboardState();
        }

        public static void AddHeldKeyBind(Keys key, ICommand command)
        {
            heldKeyBinds.Add(key, command);
        }
        public static void AddClickedKeyBind(Keys key, ICommand command)
        {
            clickedKeyBinds.Add(key, command);
        }
        public static void RemoveHeldKeyBind(Keys key, ICommand command)
        {
            heldKeyBinds.Remove(key);
        }
        public static void RemoveClickedKeyBind(Keys key, ICommand command)
        {
            clickedKeyBinds.Remove(key);
        }
        public static void EditHeldKeyBind(Keys key, ICommand newCommand)
        {
            heldKeyBinds[key] = newCommand;
        }
        public static void EditClickedKeyBind(Keys key, ICommand newCommand)
        {
            clickedKeyBinds[key] = newCommand;
        }

        public static void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach (Keys pressedKey in keyState.GetPressedKeys())
            {
                if (heldKeyBinds.TryGetValue(pressedKey, out ICommand holdCommand))
                {
                    holdCommand.Execute();
                }
                if (clickedKeyBinds.TryGetValue(pressedKey, out ICommand downCommand) & !lastKeyboardState.IsKeyDown(pressedKey))
                {
                    downCommand.Execute();
                }
            }

            lastKeyboardState = keyState;
        }
    }
}

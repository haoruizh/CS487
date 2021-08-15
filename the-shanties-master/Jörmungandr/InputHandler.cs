using Jörmungandr.Behavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    public static class InputHandler
    {
        private static Dictionary<Keys, IAction> keyBindings;
        private static Dictionary<IAction, Keys> actionBindings;
        private static Dictionary<Keys, IAction> invertedKeys;
        private static bool useInvertedKeys;

        #region Actions
        public enum PlayerAction
        {
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            SlowDown,
        }

        private static MoveLeft moveLeft;
        private static MoveRight moveRight;
        private static MoveUp moveUp;
        private static MoveDown moveDown;

        private static SlowVelocity slowDown;
        #endregion

        private static Vector2 baseVelocity;

        static InputHandler()
        {
            keyBindings = new Dictionary<Keys, IAction>();
            actionBindings = new Dictionary<IAction, Keys>();
            invertedKeys = new Dictionary<Keys, IAction>();

            useInvertedKeys = false;

            moveLeft = new MoveLeft();
            moveRight = new MoveRight();
            moveUp = new MoveUp();
            moveDown = new MoveDown();
            slowDown = new SlowVelocity();
            baseVelocity = new Vector2(10, 10);

            keyBindings[Keys.W] = moveUp;
            keyBindings[Keys.S] = moveDown;
            keyBindings[Keys.D] = moveRight;
            keyBindings[Keys.A] = moveLeft;
            keyBindings[Keys.LeftShift] = slowDown;

            actionBindings[moveUp] = Keys.W;
            actionBindings[moveDown] = Keys.S;
            actionBindings[moveLeft] = Keys.A;
            actionBindings[moveRight] = Keys.D;
            actionBindings[slowDown] = Keys.LeftShift;

        }

        /// <summary>
        /// Set a keybinding
        /// </summary>
        /// <param name="key">A Keys object (ie Keys.W)</param>
        /// <param name="action">An action enum</param>
        public static void SetKeyBinding(Keys key, PlayerAction action)
        {
            // get the old key for this action
            Keys oldKey = Keys.None;

            switch (action)
            {
                case PlayerAction.MoveLeft:
                    oldKey = actionBindings[moveLeft];
                    keyBindings[key] = moveLeft;
                    actionBindings[moveLeft] = key; // will replace the key in actionBindings
                    break;
                case PlayerAction.MoveRight:
                    oldKey = actionBindings[moveRight];
                    keyBindings[key] = moveRight;
                    actionBindings[moveRight] = key;
                    break;
                case PlayerAction.MoveUp:
                    oldKey = actionBindings[moveUp];
                    keyBindings[key] = moveUp;
                    actionBindings[moveUp] = key;
                    break;
                case PlayerAction.MoveDown:
                    oldKey = actionBindings[moveDown];
                    keyBindings[key] = moveDown;
                    actionBindings[moveDown] = key;
                    break;
                case PlayerAction.SlowDown:
                    oldKey = actionBindings[slowDown];
                    keyBindings[key] = slowDown;
                    actionBindings[slowDown] = key;
                    break;
            }

            // remove the old key from the keybindings
            RemoveKeyBinding(oldKey);
        }

        private static void RemoveKeyBinding(Keys key)
        {
            keyBindings.Remove(key);
        }

        /// <summary>
        /// Set a keybinding
        /// </summary>
        /// <param name="action">An action enum</param>
        public static Keys GetKeyBinding(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.MoveLeft:
                    return actionBindings[moveLeft];
                    break;
                case PlayerAction.MoveRight:
                    return actionBindings[moveRight];
                    break;
                case PlayerAction.MoveUp:
                    return actionBindings[moveUp];
                    break;
                case PlayerAction.MoveDown:
                    return actionBindings[moveDown];
                    break;
                case PlayerAction.SlowDown:
                    return actionBindings[slowDown];
                    break;
            }
            return Keys.None;
        }

        /// <summary>
        /// Executes the commands of pressed keys if they have a keybinding
        /// </summary>
        /// <returns></returns>
        public static void HandleInput()
        {
            KeyboardState keyboard = Keyboard.GetState();

            // special case for shift key
            if ((keyboard.IsKeyUp(Keys.LeftShift) == true) && (Player.Instance.Velocity != baseVelocity))
                keyBindings[Keys.LeftShift].UnexecuteAction(Player.Instance);

            foreach (Keys key in keyboard.GetPressedKeys())
            {
                if (keyBindings.ContainsKey(key) && !useInvertedKeys)
                    keyBindings[key].ExecuteAction(Player.Instance);
                else if (invertedKeys.ContainsKey(key) && useInvertedKeys)
                    invertedKeys[key].ExecuteAction(Player.Instance);
            }
        }

        public static void InvertControls()
        {
            // get the inverted keys
            useInvertedKeys = true;
            invertedKeys[actionBindings[moveDown]] = moveUp;
            invertedKeys[actionBindings[moveUp]] = moveDown;
            invertedKeys[actionBindings[moveLeft]] = moveRight;
            invertedKeys[actionBindings[moveRight]] = moveLeft;
        }

        public static void UnInvertControls()
        {
            useInvertedKeys = false;
        }
    }
}

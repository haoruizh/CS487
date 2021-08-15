using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    class SettingsMenu
    {
        private Texture2D menuBackground;
        private SpriteFont textFont;

        // Vector2(x, y)
        private Vector2 backgroundPos = new Vector2(100, 50);
        private Vector2 backButtonPos = new Vector2(115, 65);
        private Vector2 slowButtonPos = new Vector2(285, 285);
        private Vector2 upButtonPos = new Vector2(285, 350);
        private Vector2 leftButtonPos = new Vector2(285, 490);
        private Vector2 downButtonPos = new Vector2(285, 420);
        private Vector2 rightButtonPos = new Vector2(285, 560);

        private Vector2 pauseTextPos = new Vector2(350, 140);
        private Vector2 shootTextPos = new Vector2(320, 205);

        private Button backButton = new Button();
        private Button slowButton = new Button();
        private Button upButton = new Button();
        private Button leftButton = new Button();
        private Button downButton = new Button();
        private Button rightButton = new Button();

        private String backText;
        private String pauseText;
        private String shootText;
        private String slowText;
        private String upText;
        private String leftText;
        private String downText;
        private String rightText;

        private Keys newKey;
        private Keys currentKey;

        private bool settingsState = true;
        private bool slowButtonPressed = false;
        private bool upButtonPressed = false;
        private bool leftButtonPressed = false;
        private bool downButtonPressed = false;
        private bool rightButtonPressed = false;


        public void LoadContent(SpriteFont menuFont)
        {
            textFont = menuFont;

            menuBackground = Art.SettingsBackground;

            backText = "";
            pauseText = "ESC";
            shootText = "SPACE";
            slowText = "LEFTSHIFT";
            upText = "W";
            leftText = "A";
            downText = "S";
            rightText = "D";

            slowButton.LoadContent(menuFont, slowButtonPos, slowText);
            upButton.LoadContent(menuFont, upButtonPos, upText);
            leftButton.LoadContent(menuFont, leftButtonPos, leftText);
            downButton.LoadContent(menuFont, downButtonPos, downText);
            rightButton.LoadContent(menuFont, rightButtonPos, rightText);
            backButton.LoadContent(menuFont, backButtonPos, backText);

        }

        public void Update()
        {

            MouseState mouse = Mouse.GetState();

            backButton.Update(false, true);
            //slowButton.Update(slowButtonPressed, false);
            upButton.Update(upButtonPressed, false);
            leftButton.Update(leftButtonPressed, false);
            downButton.Update(downButtonPressed, false);
            rightButton.Update(rightButtonPressed, false);


            if (backButton.isButtonClicked())
            {
                settingsState = false;
            }
            else
            {
                settingsState = true;
            }

            if (slowButton.isButtonClicked())
            {
                if (slowButtonPressed == true)
                    slowButtonPressed = false;
                else
                {
                    slowButtonPressed = true;
                    upButtonPressed = false;
                    leftButtonPressed = false;
                    downButtonPressed = false;
                    rightButtonPressed = false;

                }
            }

            if (upButton.isButtonClicked())
            {
                if (upButtonPressed == true)
                    upButtonPressed = false;
                else
                {
                    upButtonPressed = true;
                    slowButtonPressed = false;
                    leftButtonPressed = false;
                    downButtonPressed = false;
                    rightButtonPressed = false;
                }
            }

            if (leftButton.isButtonClicked())
            {
                if (leftButtonPressed == true)
                    leftButtonPressed = false;
                else
                {
                    leftButtonPressed = true;
                    upButtonPressed = false;
                    slowButtonPressed = false;
                    downButtonPressed = false;
                    rightButtonPressed = false;
                }
            }

            if (downButton.isButtonClicked())
            {
                if (downButtonPressed == true)
                    downButtonPressed = false;
                else
                {
                    downButtonPressed = true;
                    slowButtonPressed = false;
                    upButtonPressed = false;
                    leftButtonPressed = false;
                    rightButtonPressed = false;
                }
            }

            if (rightButton.isButtonClicked())
            {
                if (rightButtonPressed == true)
                    rightButtonPressed = false;
                else
                {
                    rightButtonPressed = true;
                    slowButtonPressed = false;
                    upButtonPressed = false;
                    leftButtonPressed = false;
                    downButtonPressed = false;
                }
            }

            if (slowButtonPressed)
            {
                currentKey = InputHandler.GetKeyBinding(InputHandler.PlayerAction.SlowDown);
                newKey = getNewKey(currentKey);
                if (newKey != Keys.None)
                {
                    InputHandler.SetKeyBinding(newKey, InputHandler.PlayerAction.SlowDown);
                    if (newKey == Keys.LeftShift)
                    {
                        slowButton.changeText("LEFTSHIFT");
                    }
                    else
                    {
                        slowButton.changeText(newKey.ToString().ToUpper());
                    }
                }
            }

            if (upButtonPressed)
            {
                currentKey = InputHandler.GetKeyBinding(InputHandler.PlayerAction.MoveUp);
                newKey = getNewKey(currentKey);
                if (newKey != Keys.None)
                {
                    InputHandler.SetKeyBinding(newKey, InputHandler.PlayerAction.MoveUp);
                    if (newKey == Keys.W)
                    {
                        upButton.changeText("W");
                    }
                    else
                    {
                        upButton.changeText(newKey.ToString().ToUpper());
                    }
                }
            }

            if (leftButtonPressed)
            {
                currentKey = InputHandler.GetKeyBinding(InputHandler.PlayerAction.MoveLeft);
                newKey = getNewKey(currentKey);
                if (newKey != Keys.None)
                {
                    InputHandler.SetKeyBinding(newKey, InputHandler.PlayerAction.MoveLeft);
                    if (newKey == Keys.A)
                    {
                        leftButton.changeText("A");
                    }
                    else
                    {
                        leftButton.changeText(newKey.ToString().ToUpper());
                    }
                }
            }

            if (downButtonPressed)
            {
                currentKey = InputHandler.GetKeyBinding(InputHandler.PlayerAction.MoveDown);
                newKey = getNewKey(currentKey);
                if (newKey != Keys.None)
                {
                    InputHandler.SetKeyBinding(newKey, InputHandler.PlayerAction.MoveDown);
                    if (newKey == Keys.S)
                    {
                        downButton.changeText("S");
                    }
                    else
                    {
                        downButton.changeText(newKey.ToString().ToUpper());
                    }
                }
            }

            if (rightButtonPressed)
            {
                currentKey = InputHandler.GetKeyBinding(InputHandler.PlayerAction.MoveRight);
                newKey = getNewKey(currentKey);
                if (newKey != Keys.None)
                {
                    InputHandler.SetKeyBinding(newKey, InputHandler.PlayerAction.MoveRight);
                    if (newKey == Keys.D)
                    {
                        rightButton.changeText("D");
                    }
                    else
                    {
                        rightButton.changeText(newKey.ToString().ToUpper());
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackground, backgroundPos, Color.White);
            spriteBatch.DrawString(textFont, pauseText, pauseTextPos, Color.White);
            spriteBatch.DrawString(textFont, shootText, shootTextPos, Color.White);
            slowButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
            upButton.Draw(spriteBatch);
            leftButton.Draw(spriteBatch);
            downButton.Draw(spriteBatch);
            rightButton.Draw(spriteBatch);
        }

        public bool closeSettings()
        {
            if (settingsState == true)
                return true;
            else
                return false;
        }

        private Keys getNewKey(Keys currentKey)
        {
            Keys newKey;
            Keys[] keyArray;

            KeyboardState keyboardState = Keyboard.GetState();
            keyArray = keyboardState.GetPressedKeys();

            if (keyArray.Length > 0)
            {
                newKey = keyArray[0];
                return newKey;
            }
            else
            {
                return Keys.None;
            }
        }
    }
}

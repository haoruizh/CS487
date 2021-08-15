using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    class Button
    {
        private Texture2D buttonTexture = Art.MenuButton;
        private SpriteFont textFont;

        private Vector2 buttonPosition = new Vector2(0, 0);
        private Vector2 mousePosition = new Vector2(0, 0);

        private bool buttonClicked = false;
        private bool mouseOverButton = false;
        private bool leftClick = false;
        private bool editing = false;
        private bool small = false;

        private String text;


        public void LoadContent(SpriteFont menuFont, Vector2 buttonPos, String buttonText)
        {
            textFont = menuFont;
            buttonPosition = buttonPos;
            text = buttonText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouse">the state of the mouse</param>
        public void Update(bool checkEdit, bool checkSmall)
        {
            editing = checkEdit;
            small = checkSmall;

            MouseState mouse = Mouse.GetState();

            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;

            // Check if a mouse is over the button
            if ((mousePosition.X >= buttonPosition.X && mousePosition.X <= buttonPosition.X + buttonTexture.Width)
                && (mousePosition.Y >= buttonPosition.Y && mousePosition.Y <= buttonPosition.Y + buttonTexture.Height))
            {
                mouseOverButton = true;
            }
            else
            {
                mouseOverButton = false;
            }

            // Check if left mouse button has been pressed
            if (mouse.LeftButton == ButtonState.Pressed)
                leftClick = true;
            else
                leftClick = false;

            // Button clicked if the left mouse button has been pressed and mouse is over the button
            if (mouseOverButton == true && leftClick == true)
                buttonClicked = true;
            else
                buttonClicked = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (small == true)
            {
                buttonTexture = Art.SmallMenuButton;
            }
            else
            {
                if (editing == false)
                    buttonTexture = Art.MenuButton;
                else if (editing == true)
                    buttonTexture = Art.EditMenuButton;
            }

            spriteBatch.Draw(buttonTexture, buttonPosition, Color.White);

            Vector2 textSize = textFont.MeasureString(text);
            Vector2 textPosition = new Vector2(buttonPosition.X + (buttonTexture.Width / 2) - (textSize.X / 2), buttonPosition.Y + 10);

            spriteBatch.DrawString(textFont, text, textPosition, Color.White);
        }

        public void changeText(String newText)
        {
            text = newText;
        }

        public bool isButtonClicked()
        {
            if (buttonClicked == true)
            {
                return true;
            }
            else
                return false;
        }

    }
}

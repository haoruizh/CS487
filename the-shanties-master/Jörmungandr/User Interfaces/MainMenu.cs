using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    class MainMenu
    {

        private Texture2D menuBackground;

        private Vector2 center = new Vector2(100, 50);
        private Vector2 topButtonPos = new Vector2(200, 200);
        private Vector2 midButtonPos = new Vector2(200, 350);
        private Vector2 botButtonPos = new Vector2(200, 500);

        private Button startButton = new Button();
        private Button settingsButton = new Button();
        private Button endButton = new Button();

        private bool playState = false;
        private bool settingsState = false;
        private bool exitState = false;

        public void LoadContent(SpriteFont menuFont)
        {
            // Content for the Main Menu
            menuBackground = Art.MenuBackground;

            startButton.LoadContent(menuFont, topButtonPos, "START");
            settingsButton.LoadContent(menuFont, midButtonPos, "SETTINGS");
            endButton.LoadContent(menuFont, botButtonPos, "EXIT");
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            startButton.Update(false, false);
            settingsButton.Update(false, false);
            endButton.Update(false, false);

            if (startButton.isButtonClicked())
                playState = true;
            else
                playState = false;

            if (settingsButton.isButtonClicked())
                settingsState = true;
            else
                settingsState = false;

            if (endButton.isButtonClicked())
                exitState = true;
            else
                exitState = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackground, center, Color.White);
            startButton.Draw(spriteBatch);
            settingsButton.Draw(spriteBatch);
            endButton.Draw(spriteBatch);
        }

        /// <summary>
        /// For starting the game
        /// </summary>
        /// <returns>returns true if the game should start, false otherwise</returns>
        public bool playGame()
        {
            if (playState == true)
                return true;
            else
                return false;
        }

        /// <summary>
        /// For exiting the game
        /// </summary>
        /// <returns>returns true if the game should close, false otherwise</returns>
        public bool closeGame()
        {
            if (exitState == true)
                return true;
            else
                return false;
        }

        /// <summary>
        /// For allowing the user to change the controls
        /// </summary>
        /// <returns>returns true if the settings menu should be shown, false otherwise</returns>
        public bool openSettings()
        {
            if (settingsState == true)
            {
                settingsState = false;
                return true;
            }
            else
                return false;
        }


    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    class PauseMenu
    {
        private Texture2D menuBackground;

        private Vector2 backgroundPos = new Vector2(100, 150);
        private Vector2 topButtonPos = new Vector2(200, 300);
        private Vector2 midButtonPos = new Vector2(200, 450);

        private Button resumeButton = new Button();
        private Button quitButton = new Button();


        private bool pauseState = false;
        private bool exitState = false;

        public void LoadContent(SpriteFont menuFont)
        {
            menuBackground = Art.PauseBackground;

            resumeButton.LoadContent(menuFont, topButtonPos, "RESUME");
            quitButton.LoadContent(menuFont, midButtonPos, "QUIT");
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            resumeButton.Update(false, false);
            quitButton.Update(false, false);

            if (resumeButton.isButtonClicked())
                pauseState = false;

            if (quitButton.isButtonClicked())
                exitState = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackground, backgroundPos, Color.White);
            resumeButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);

        }

        /// <summary>
        /// Allows for Game.cs to pause and update the menu UIs
        /// </summary>
        public void updatePauseState()
        {
            if (pauseState == true)
                pauseState = false;
            else
                pauseState = true;
        }

        /// <summary>
        /// This function is used in the Game.cs update() method so the game goes into a different conditional
        /// </summary>
        public bool checkPausedGame()
        {
            if (pauseState == true)
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
    }
}

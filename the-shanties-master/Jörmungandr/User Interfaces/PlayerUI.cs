using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.IO;

namespace Jörmungandr
{
    public class PlayerUI
    {
        private Texture2D emptyHeart;
        private Texture2D fullHeart;
        private Texture2D fullHeart2;
        private Texture2D fullHeart3;
        private Texture2D UIBar;
        private Texture2D UIWinBox;
        private Texture2D UILoseBox;

        private SpriteFont scoreFont;
        private SpriteFont highscoreFont;
        private SpriteFont loserPopup;
        private SpriteFont winnerPopup;
        private SpriteFont command;

        private Vector2 posOne = new Vector2(534, 0);
        private Vector2 posTwo = new Vector2(468, 0);
        private Vector2 posThree = new Vector2(402, 0);
        private Vector2 posFour = new Vector2(336, 0);
        private Vector2 middleOfScreen = new Vector2(200, 400);
        private Vector2 lowerMiddleOfScreen = new Vector2(210, 445);
        private Vector2 boxPosition = new Vector2(180, 390);

        private int score;
        private int highscore;
        private int lives;
        private float totalGameTime;
        private bool expired;

        public bool finalBossDead = false;
        public PlayerUI()
        {
            this.highscore = this.LoadHighScore();
            this.lives = 3;
        }

        /// <summary>
        /// Load function to set the local variables needed for the UI that are from Game.cs
        /// </summary>
        /// <param name="font"></param>
        /// <param name="newScore"></param>
        /// <param name="newLives"></param>
        public void LoadContent(SpriteFont font, SpriteFont endGame, SpriteFont endGameFont)
        {
            this.scoreFont = font;
            this.highscoreFont = font;
            this.loserPopup = endGame;
            this.winnerPopup = endGame;
            this.command = endGameFont;
            UIBar = Art.TopUIBar;
            UIWinBox = Art.UIWinBox;
            UILoseBox = Art.UILoseBox;
            fullHeart = Art.FullHeart;
            fullHeart2 = Art.FullHeart;
            fullHeart3 = Art.FullHeart;
            emptyHeart = Art.EmptyHeart;
        }

        public void UpdateScore(int newScore)
        {
            this.score = newScore;
            if (this.score > this.highscore)
                this.highscore = this.score;
        }

        public void UpdateLives(int newLives)
        {
            this.lives = newLives;
            switch (lives)
            {
                case 0:
                default:
                    this.fullHeart = Art.EmptyHeart;
                    this.fullHeart2 = Art.EmptyHeart;
                    this.fullHeart3 = Art.EmptyHeart;
                    break;
                case 1:
                    this.fullHeart = Art.FullHeart;
                    this.fullHeart2 = Art.EmptyHeart;
                    this.fullHeart3 = Art.EmptyHeart;
                    break;
                case 2:
                    this.fullHeart = Art.FullHeart;
                    this.fullHeart2 = Art.FullHeart;
                    this.fullHeart3 = Art.EmptyHeart;
                    break;
                case 3:
                    this.fullHeart = Art.FullHeart;
                    this.fullHeart2 = Art.FullHeart;
                    this.fullHeart3 = Art.FullHeart;
                    break;
            }
        }

        /// <summary>
        /// This function will draw everything in the player UI
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, float totalGameTime)
        {
            spriteBatch.Draw(UIBar, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(highscoreFont, "Highscore: " + highscore, new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(0, 30), Color.Black);
            spriteBatch.Draw(emptyHeart, posOne, Color.White);
            spriteBatch.Draw(emptyHeart, posTwo, Color.White);
            spriteBatch.Draw(emptyHeart, posThree, Color.White);
            spriteBatch.Draw(fullHeart3, posOne, Color.White);
            spriteBatch.Draw(fullHeart2, posTwo, Color.White);
            spriteBatch.Draw(fullHeart, posThree, Color.White);

            if(this.lives <= 0)
            {
                spriteBatch.Draw(UILoseBox, boxPosition, Color.White);
                spriteBatch.DrawString(loserPopup, "YOU LOST", middleOfScreen, Color.White);
                spriteBatch.DrawString(command, "(press ESC to quit)", lowerMiddleOfScreen, Color.White);
                SaveHighScore(this.highscore);
            }

            // When the game time reaches the number specified below, the following string will be drawn
            if ((totalGameTime >= 180 && this.lives != 0) || (finalBossDead))
            {
                spriteBatch.Draw(UIWinBox, boxPosition, Color.White);
                spriteBatch.DrawString(winnerPopup, "YOU WON", middleOfScreen, Color.White);
                spriteBatch.DrawString(command, "(press ESC to quit)", lowerMiddleOfScreen, Color.White);
                SaveHighScore(this.highscore);
            }
        }

        private int LoadHighScore()
        {
            int score;
            return File.Exists("HighScore.txt") && int.TryParse(File.ReadAllText("HighScore.txt"), out score) ? score : 0;
        }

        private void SaveHighScore(int score)
        {
            File.WriteAllText("HighScore.txt", score.ToString());
        }

    }
}

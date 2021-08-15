using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Jörmungandr
{
    /// <summary>
    /// Handles the background movement.
    /// </summary>
    class Background
    {
        private Texture2D _currBackground;
        private Texture2D _backgroundCave;
        private Texture2D _backgroundCaveToOcean;
        private Texture2D _backgroundOcean;
        private Texture2D _backgroundOceanToSky;
        private Texture2D _backgroundSky;
        private int switchWait = 0;
        private int switchVal = 0;
        private int switchTo = 0;
        public Rectangle Size;

        private Vector2 posOne = new Vector2(0, 0);
        private Vector2 posTwo = new Vector2(0, 0);
        private Vector2 posThree = new Vector2(0, 0);

        private Vector2 posCaveToOcean = new Vector2(0, -1600);
        private Vector2 posOceanToSky = new Vector2(0, -1600);


        /// <summary>
        /// Loads the bg images into the sprites.
        /// </summary>
        public void LoadBackground()
        {
            _backgroundCave = Art.CaveBackground;
            _backgroundCaveToOcean = Art.CaveToOceanBackground;
            _backgroundOcean = Art.OceanBackground;
            _backgroundOceanToSky = Art.OceanToSkyBackground;
            _backgroundSky = Art.SkyBackground;

            Size = new Rectangle(0, 0, 600, 800);

            posTwo = new Vector2(0, Size.Height + posOne.Y);
            posThree = new Vector2(0, Size.Height + posTwo.Y);
        }
        public void SwitchBackground(Texture2D background)
        {
            _currBackground = background;

            Size = new Rectangle(0, 0, _currBackground.Width, _currBackground.Height);
            posOne = new Vector2(0, -_currBackground.Height);
            posTwo = new Vector2(0, -_currBackground.Height - posOne.Y);
            posThree = new Vector2(0, -_currBackground.Height - posTwo.Y);

        }

        public void SwitchTo(int x)
        {
            switchVal = x;
            switchWait = 1;
        }

        /// <summary>
        /// Draws the bg images.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currBackground, posOne,
                new Rectangle(0, 0, _currBackground.Width, _currBackground.Height), Color.White,
                0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_currBackground, posTwo,
                new Rectangle(0, 0, _currBackground.Width, _currBackground.Height), Color.White,
                0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_currBackground, posThree,
                new Rectangle(0, 0, _currBackground.Width, _currBackground.Height), Color.White,
                0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

            if (switchTo == 1 && switchWait == 0)
            {
                spriteBatch.Draw(_backgroundCaveToOcean, posCaveToOcean,
                   new Rectangle(0, 0, _backgroundCaveToOcean.Width, _backgroundCaveToOcean.Height), Color.White,
                   0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
            if (switchTo == 2 && switchWait == 0)
            {
                spriteBatch.Draw(_backgroundOceanToSky, posOceanToSky,
                   new Rectangle(0, 0, _backgroundOceanToSky.Width, _backgroundOceanToSky.Height), Color.White,
                   0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Scrolls the bg.
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            if (switchTo == 0)
            {
                if ((switchWait != 0) && ((Math.Floor(posOne.Y) == Size.Height) || (Math.Floor(posTwo.Y) == Size.Height) || (Math.Floor(posThree.Y) == Size.Height)))
                {
                    switchTo = switchVal;
                    switchWait = 0;
                }
                else if (posOne.Y > Size.Height)
                { posOne.Y = posThree.Y - Size.Height; }

                else if (posTwo.Y > Size.Height)
                { posTwo.Y = posOne.Y - Size.Height; }

                else if (posThree.Y > Size.Height)
                { posThree.Y = posTwo.Y - Size.Height; }
            }
            if ((posCaveToOcean.Y >= 0) && (switchTo == 1) && (switchWait == 0))
            {
                switchTo = 0;
                _currBackground = _backgroundOcean;
                posOne = new Vector2(0, -_currBackground.Height);
                posTwo = new Vector2(0, -_currBackground.Height - posOne.Y);
                posThree = new Vector2(0, -_currBackground.Height - posTwo.Y);
            }
            if ((posOceanToSky.Y >= 0) && (switchTo == 2) && (switchWait == 0))
            {
                switchTo = 0;
                _currBackground = _backgroundSky;
                posOne = new Vector2(0, -_currBackground.Height);
                posTwo = new Vector2(0, -_currBackground.Height - posOne.Y);
                posThree = new Vector2(0, -_currBackground.Height - posTwo.Y);
            }

            Vector2 direction = new Vector2(0, 1);
            Vector2 speed = new Vector2(0, 150);

            posOne += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds;
            posTwo += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds;
            posThree += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds;
            if (switchTo == 1)
            { posCaveToOcean += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds; }
            if (switchTo == 2)
            { posOceanToSky += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds; }
        }
    }
}

using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Jörmungandr
{
    /// <summary>
    /// Player classes for character that  player controls
    /// </summary>
    public class Player : Character
    {
        private Vector2 spawnLocation;
        private bool IsUnderAffect = false;
        private int lives;
        private int score;

        private static Player instance;

        /// <summary>
        /// set instance property to an instance of player.
        /// </summary>
        public static Player Instance
        {
            get
            {
                if (instance == null)
                { 
                    // get the player info from json file
                    //string infoFromFile;
                    //string JSONFilePath = "C:/Users/haoru/Desktop/CS487/project/the-shanties/Jörmungandr/JSON/Player.json";
                    //using (var reader = new StreamReader(JSONFilePath))
                    //{
                    //    infoFromFile = reader.ReadToEnd();
                    //}
                    //var playerInfo = JsonConvert.DeserializeObject < Dictionary<string, string>>(infoFromFile);

                    instance = new Player(new PlayerBulletFactory(), Art.Boat, new Vector2(300, 750));
                }
                return instance;
            }
        }

        /// <summary>
        /// Set current position to spawn position.
        /// </summary>
        internal void SetRespawnPosition()
        {
            this.Position = spawnLocation;
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="bulletFactory">Player bullet factory</param>
        /// <param name="image">player sprite image</param>
        /// <param name="position">player spawn position</param>
        public Player(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position) : base(bulletFactory, image, position)
        {
            this.spawnLocation = position;
            this.lives = 3;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.sprite.Width/2), (int)(this.sprite.Height/2));
            this.Velocity = new Vector2(10, 10);
        }

        /// <summary>
        /// Draw the player sprite
        /// </summary>
        /// <param name="spriteBatch">sprite drawer.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //  Rectangle sourceRectangle = new Rectangle(100, 100, 50, 50);
            spriteBatch.Draw(this.sprite, Position, null, color, Orientation, new Vector2(0,0), .5f, 0, 0);
        }

        /// <summary>
        /// update players movement, status, and action
        /// </summary>
        /// <param name = "gt" ></ param >
        /// // this part is for demo only.
        // bad design and implementation. Reconstruct required
        // For now: hard-code to get keyboard input and corresponding behavior. Very risky and difficult to change control
        // further change: make player behavior pattern handle this.
        public override void Update(GameTime gt)
        {
            // if the user is dead
            if (this.IsExpired)
            {
                return;
            }

            // otherwise
            // update player movement
            InputHandler.HandleInput();

            // TODO: move to inputHandler
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (timeLastShot > 0)
                {
                    timeLastShot -= gt.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    // fire the shots
                    this.Fire();
                    timeLastShot = .25;
                }
            }
            bulletFactory.Position.X = this.Position.X;
            bulletFactory.Position.Y = this.Position.Y;

            // check if player reaches the boundary of screen
            if (this.Position.X >= 600)
            {
                this.Position.X = 600;
            }

            if (this.Position.X <= 0)
            {
                this.Position.X = 0;
            }

            if (this.Position.Y <= 0)
            {
                this.Position.Y = 0;
            }

            base.Update(gt);
        }

        /// <summary>
        /// Set the bullet factory to a new bullet factory
        /// </summary>
        /// <param name="newFactory">New bullet factory</param>
        internal void SetFactory(AbstractBulletFactory newFactory)
        {
            this.bulletFactory = newFactory;
        }

        /// <summary>
        /// increase Lives property by 1 if it's smaller than 3
        /// </summary>
        internal void RecoverLife()
        {
            if(lives < 3)
            {
                lives += 1;
            }
            Game.gameInstance.playerUI.UpdateLives(lives);
        }

        /// <summary>
        /// Fire bullets from bullet factory
        /// </summary>
        protected override void Fire()
        {
            EntityManager.Add(this.bulletFactory.CreateBullet(this.bulletFactory.Position, 270));
        }

        /// <summary>
        /// Reduce life by 1 and check if the player can be expired. Update the UI
        /// </summary>
        internal void LoseLife()
        {
            lives -= 1;

            if (lives <= 0)
                this.Expire();

            Game.gameInstance.playerUI.UpdateLives(lives);
        }

        /// <summary>
        /// Update the score and reflect changes to UI
        /// </summary>
        /// <param name="newScore"></param>
        internal void UpdateScore(int newScore)
        {
            this.score += newScore;
            Game.gameInstance.playerUI.UpdateScore(this.score);
        }
    }
}

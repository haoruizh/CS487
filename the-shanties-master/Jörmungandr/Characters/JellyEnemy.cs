using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Jörmungandr.Behavior;

namespace Jörmungandr
{
    /// <summary>
    /// JellyEnemy class. 
    /// Spawns randomly on right side of screen, jumps up and down
    /// Shoots 3 JellyBullets (at 60, 90, and 120 degrees from the jelly position)
    /// </summary>
    public class JellyEnemy : Enemy
	{
		public static Random rand = new Random();

        // Constructor for a JellyEnemy
        public JellyEnemy(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
        }

        // Updates a JellyEnemy, the behavior is updated as well as collision gets checked
        public override void Update(GameTime gt)
        {
            if (timeUntilStart <= 0)
            {
                ApplyBehaviors();
                timeUntilStart = 1;
            }
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }

            Position += Velocity;

            if (Position.X > 600 || this.IsExpired)
            {
                this.Expire();
                // To-do: Update health, handle collision
            }
            // if it is in proximity of the player, handle collision
            if (Vector2.Distance(this.Position, Player.Instance.Position) <= 30)
            {
                this.Expire(); // change to handle collision
            }

            if (timeLastShot > 0)
            {
                timeLastShot -= gt.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // fire the shots
                this.Fire();
                timeLastShot = 3;
            }

            Velocity *= .8f;

            base.Update(gt);
        }

        #region Behaviors

        protected override void Fire()
        {
            EntityManager.Add(bulletFactory.CreateBullet(this.Position, 90));
            EntityManager.Add(bulletFactory.CreateBullet(this.Position, 120));
            EntityManager.Add(bulletFactory.CreateBullet(this.Position, 60));
        }

        #endregion
    }
}

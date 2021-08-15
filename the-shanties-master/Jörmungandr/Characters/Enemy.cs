using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Jörmungandr.Bullets.Factory;

namespace Jörmungandr
{
    /// <summary>
    /// Enemy class. All other enemies should inherit from this class.
    /// </summary>
    public abstract class Enemy : Character
    {
        protected int timeUntilStart = 60;
        public bool didExplode = false;

        // how many points the enemy is worth when killed
        protected int pointValue = 50;
        public int PointValue => this.pointValue;

        /// <summary>
        /// basic enemy constructor
        /// </summary>
        /// <param name="bulletFactory">bullet factory for this enemy</param>
        /// <param name="image">sprite of this enemy</param>
        /// <param name="position">spawn position</param>
        /// <param name="points">reward points</param>
        /// <param name="fireRate">fire rate</param>
        /// <param name="health">health of this enemy</param>
        public Enemy(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health):base(bulletFactory, image, position)
        {
            this.pointValue = points;
            this.timeLastShot = fireRate;
            this.health = health;
        }

        /// <summary>
        /// reduce this health by 1 and check if it can be expired.
        /// </summary>
        public void LoseLife()
        {
            this.health -= 1;
            if (this.health <= 0)
                this.Expire();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr.Bullets.Factory;
using Jörmungandr.Behavior;

namespace Jörmungandr
{
    public class RockEnemy : Enemy
    {
        Vector2 ScreenSize = new Vector2(600, 800);
        public static Random rand = new Random();

        /// <summary>
        /// Rock enemy constructor
        /// </summary>
        /// <param name="bulletFactory">Rock enemy bullet factory</param>
        /// <param name="image">Rock enemy sprite</param>
        /// <param name="position">Rock enemy spawn position</param>
        /// <param name="points">Rock enemy reward points</param>
        /// <param name="fireRate">Rock enemy fire rate</param>
        /// <param name="health">Rock enemy health</param>
        public RockEnemy(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
        }

        /// <summary>
        /// Update status based on Game time
        /// </summary>
        /// <param name="gt">Game time varaible</param>
        public override void Update(GameTime gt)
        {
            if (this.timeUntilStart <= 0)
            {
                ApplyBehaviors();
            }
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }
            Position += Velocity;
            Velocity *= 1.0f;

            base.Update(gt);

            if (Position.Y >= 830 || this.IsExpired)
            {
                this.Expire();
            }
            // if it is in proximity of the player, collision
            if (Vector2.Distance(this.Position, Player.Instance.Position) <= 30)
            {
                this.Expire(); // change to handle collision
            }
        }

        #region Behaviors

        // should not be called
        protected override void Fire()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

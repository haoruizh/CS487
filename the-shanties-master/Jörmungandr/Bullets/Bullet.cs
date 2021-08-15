using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets
{

    /// <summary>
    /// abstract template for all bullet classes (players, enemy)
    /// </summary>
    public abstract class Bullet : Entity
    {

        protected bool isEnemy;
        protected bool canKillBullet;
        // bullet hitbox
        protected Rectangle hitbox;

        /// <summary>
        /// Bullet constructor
        /// </summary>
        /// <param name="position"> the spawn position of the bullet. </param>
        public Bullet(Vector2 position)
        {
            this.Position = position;
            canKillBullet = false;
            isEnemy = true;
        }

        /// <summary>
        /// Get bullet hit box.
        /// </summary>
        /// <returns>return the bullet hit box.</returns>
        public Rectangle GetHitbox()
        {
            return this.hitbox;
        }

        /// <summary>
        /// moves the bullet  in the direction of the orientation
        /// </summary>
        /// <param name="gt"></param>
        public override void Update(GameTime gt)
        {

            // movement
            Vector2 direction = new Vector2((float)Math.Cos(Orientation), (float)Math.Sin(Orientation));
            direction.Normalize();
            this.Position += direction * Velocity;

            // remove the bullet if it goes out of bounds
            if (Position.Y > 800)
            {
                this.Expire();
            }

            this.hitbox.X = (int)this.Position.X;
            this.hitbox.Y = (int)this.Position.Y;
        }

        /// <summary>
        /// Draws the bullet
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite,
                new Rectangle((int)Position.X, (int)Position.Y, sprite.Width, sprite.Height), null,
                Color.White,
                Orientation,
                new Vector2(0,0),
                SpriteEffects.None, 0);
        }

        /// <summary>
        /// Return if this bullet is spawned by enemy.
        /// </summary>
        /// <returns>return isEnemy property of bullet</returns>
        public bool IsEnemy()
        {
            return this.isEnemy;
        }

        /// <summary>
        /// Check if this bullet hit by given target bullet.
        /// </summary>
        /// <param name="targetBullet">Given target bullet.</param>
        /// <returns>if this bullet's hitbox intersects with given targetBullet.</returns>
        internal bool IsHit(Bullet targetBullet)
        {
            if(this.hitbox.Intersects(targetBullet.GetHitbox()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if this bullet can kill other bullet
        /// </summary>
        /// <returns>this bullet canKillBullet property.</returns>
        public bool CanKillBullets()
        {
            return this.canKillBullet;
        }

        /// <summary>
        /// Set this bullet canKillBullet property.
        /// </summary>
        /// <param name="canKillBullet">new canKillBullet bool value.</param>
        internal void SetKillBullet(bool canKillBullet)
        {
            this.canKillBullet = canKillBullet;
        }
    }

}

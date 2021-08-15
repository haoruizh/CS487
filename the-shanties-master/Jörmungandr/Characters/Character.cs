using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Jörmungandr.Bullets;
using Jörmungandr.Bullets.Factory;

namespace Jörmungandr
{
    /// <summary>
    /// abstract template for all character classes (players, enemy)
    /// </summary>
    public abstract class Character : Entity
    {
        protected double health;

        // how often the character can fire bullets
        protected double timeLastShot;

        // handles the firing of bullets
        protected AbstractBulletFactory bulletFactory;

        // the hitbox of the character
        protected Rectangle hitbox;

        /// <summary>
        /// Character Constructor
        /// </summary>
        /// <param name="bulletFactory">Bullet factory</param>
        /// <param name="image">character sprite</param>
        /// <param name="position">character spawn position.</param>
        public Character(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position)
        {
            this.bulletFactory = bulletFactory;
            this.sprite = image;
            this.Position = position;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.sprite.Width), (int)(this.sprite.Height));
        }

        /// <summary>
        /// Update hitbox position based on gametime
        /// </summary>
        /// <param name="gt">Game time</param>
        public override void Update(GameTime gt)
        {
            this.hitbox.X = (int)this.Position.X;
            this.hitbox.Y = (int)this.Position.Y;
        }

        public bool IsHit(Bullet target)
        {
            if (this.hitbox.Intersects(target.GetHitbox()))
            {
                //Console.Beep();
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if this character is collide with given item
        /// </summary>
        /// <param name="target">given item</param>
        /// <returns>if character and item hitboxes intersects.</returns>
        public bool IsCollideWithItem(Item.BasicItem target)
        {
            if (target.GetHitBox().Intersects(this.hitbox))
            {
                //Console.Beep();
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if this character is collide with other character
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsCollideWithCharacter(Character target)
        {
            if (this.hitbox.Intersects(target.hitbox))
            {
                //Console.Beep();
                return true;
            }
            return false;
        }

        protected abstract void Fire();
    }
}

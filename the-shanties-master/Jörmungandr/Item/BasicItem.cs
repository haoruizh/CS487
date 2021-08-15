using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item
{
    public abstract class BasicItem:Entity
    {
        protected List<IEnumerator<int>> behaviors = new List<IEnumerator<int>>();
        protected int timeUntilStart = 60;
        protected Rectangle hitbox;
        public BasicItem(Texture2D image, Vector2 position) :base()
        {
            this.sprite = image;
            this.Position = position;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.sprite.Width), (int)(this.sprite.Height));
        }

        internal Rectangle GetHitBox()
        {
            return this.hitbox;
        }

        /// <summary>
        /// Update current item
        /// </summary>
        /// <param name="gt">game time</param>
        public override void Update(GameTime gt)
        {
            this.hitbox.X = (int)this.Position.X;
            this.hitbox.Y = (int)this.Position.Y;
        }

        public abstract void DoAffect(Player curPlayer);
        public abstract void DoAffect(Enemy curEnemy);
        public abstract void UnAffect(Player curPlayer);
        public abstract void UnAffect(Enemy curEnemy);

    }
}

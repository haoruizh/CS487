using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets
{
    public class PlayerBullet : Bullet
    {
        public PlayerBullet(Vector2 position, float orientation):base(position)
        {
            this.isEnemy = false;
            this.sprite = Art.MineBullet;
            this.canKillBullet = false;
            this.Velocity = new Vector2(0, 10);
            this.Orientation = MathHelper.ToRadians(orientation);
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));
        }
    }
}

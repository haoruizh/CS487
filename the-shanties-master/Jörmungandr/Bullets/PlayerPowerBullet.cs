using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets
{
    public class PlayerPowerBullet:Bullet
    {
        public PlayerPowerBullet(Vector2 position, float orientation) : base(position)
        {
            this.isEnemy = false;
            this.sprite = Art.MineBullet;
            this.canKillBullet = true;
            this.Velocity = new Vector2(0, 10);
            this.Orientation = MathHelper.ToRadians(orientation);
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));
        }
    }
}

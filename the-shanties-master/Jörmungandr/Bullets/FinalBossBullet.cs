using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBullet : EnemyBullet
    {
        public FinalBossBullet(Vector2 position, float orientation):base(position, orientation)
        {
            this.sprite = Art.FinalBossBullet;
            this.Position = position;
            this.Orientation = MathHelper.ToRadians(orientation);
            this.Velocity = new Vector2(4, 4);
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));
        }
    }
}

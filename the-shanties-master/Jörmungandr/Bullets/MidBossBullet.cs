using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class MidBossBullet : EnemyBullet
    {
        public MidBossBullet(Vector2 position, float orientation) : base(position, orientation)
        {
            this.sprite = Art.FinalBossBullet;
            this.Position = position;
            this.Orientation = MathHelper.ToRadians(orientation);
            this.Velocity = new Vector2(4, 4);
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));
        }

        public override void Update(GameTime gt)
        {
            Vector2 direction = new Vector2((float)Math.Cos(Orientation), (float)Math.Sin(Orientation));
            direction.Normalize();
            this.Position += direction * Velocity;

            // remove the bullet if it goes out of bounds
            if (Position.Y > 800)
            {
                this.Expire();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBulletTargetted : Bullet
    {
        Vector2 direction = Player.Instance.Position;


        public FinalBossBulletTargetted(Vector2 position, float orientation) : base(position)
        {
            this.sprite = Art.FinalBossBullet;

            this.Position = position;
            this.Orientation = (float)(Math.Atan2(Player.Instance.Position.Y - Position.Y, Player.Instance.Position.X - Position.X));
            this.Velocity = new Vector2(3f, 3f);
            this.direction = Player.Instance.Position - Position;
            direction.Normalize();
            this.isEnemy = true;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));

        }

        public override void Update(GameTime gt)
        {
            this.Position += direction * Velocity;

            // remove the bullet if it goes out of bounds
            if (Position.Y > 800)
            {
                this.Expire();
            }

            this.hitbox.X = (int)this.Position.X;
            this.hitbox.Y = (int)this.Position.Y;
        }
    }
}

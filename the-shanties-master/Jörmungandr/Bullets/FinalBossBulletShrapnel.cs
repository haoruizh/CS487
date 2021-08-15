using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBulletShrapnel : Bullet
    {
        private static double timeAlive = 0;
        public static Random rand = new Random();
        double createTime;
        public FinalBossBulletShrapnel(Vector2 position, float orientation) : base(position)
        {
            this.sprite = Art.FinalBossBullet;
            this.createTime = 0;
            this.Position = position;
            this.Orientation = MathHelper.ToRadians(orientation);
            this.Velocity = new Vector2((float)3, (float)3);
            this.isEnemy = true;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));

        }

        public override void Update(GameTime gt)
        {

            Vector2 direction = new Vector2((float)Math.Cos(Orientation), (float)Math.Sin(Orientation));
            direction.Normalize();
            this.Position += direction * Velocity;
            timeAlive += gt.ElapsedGameTime.TotalSeconds;
            Vector2 myPos = this.Position;
            double myTimeAlive = timeAlive;
            if (Velocity.X > 2)
            {
                this.Velocity -= new Vector2((float)(createTime) / 6, (float)(createTime) / 6);
            }
            this.createTime += gt.ElapsedGameTime.TotalSeconds;
            if ((createTime > 1) && (myPos.Y < 200))
            {
                EntityManager.Add(new FinalBossBulletShrapnel(myPos, 60 + rand.Next(-40, 40)));
                EntityManager.Add(new FinalBossBulletShrapnel(myPos, 120 + rand.Next(-40, 40)));
                this.Expire();
            }
            else if (createTime > 2)
            {
                EntityManager.Add(new FinalBossBullet(myPos, 60 + rand.Next(-40, 40)));
                EntityManager.Add(new FinalBossBullet(myPos, 120 + rand.Next(-40, 40)));
                this.Expire();
            }

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

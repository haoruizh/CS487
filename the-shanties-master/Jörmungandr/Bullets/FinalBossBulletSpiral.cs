using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBulletSpiral : Bullet
    {
        private static double timeAlive = 0;
        public static Random rand = new Random();
        double createTime;
        Vector2 originalPos;
        double shootTime;
        public FinalBossBulletSpiral(Vector2 position, float orientation) : base(position)
        {
            this.sprite = Art.FinalBossBullet;
            this.createTime = 0;
            this.shootTime = 0;
            this.Position = position;
            this.originalPos = position;
            this.Position.X = Position.X - 50;
            this.Orientation = MathHelper.ToRadians(orientation);
            this.Velocity = new Vector2((float)0, (float)0);
            this.isEnemy = true;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));

        }

        public override void Update(GameTime gt)
        {
            Vector2 direction = new Vector2((float)Math.Cos(Orientation), (float)Math.Sin(Orientation));
            direction.Normalize();
            //this.Position += direction * Velocity;
            timeAlive += gt.ElapsedGameTime.TotalSeconds;
            this.shootTime += gt.ElapsedGameTime.TotalSeconds;
            Vector2 myPos = this.Position;
            double myTimeAlive = timeAlive;
            if (Velocity.X > 2)
            {
                //this.Velocity -= new Vector2((float)(timeAlive) / 6, (float)(timeAlive) / 6);
            }
            Position = RotateAboutOrigin(Position, originalPos, 1);
            if (Math.Floor(shootTime) == 10)
            {
                EntityManager.Add(new FinalBossBullet(myPos, (float)Math.Atan2(this.Position.Y - Player.Instance.Position.Y, this.Position.X - Player.Instance.Position.X)));
                shootTime = 0;
            }
            else if (timeAlive > 1000)
            {
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

        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }
    }
}

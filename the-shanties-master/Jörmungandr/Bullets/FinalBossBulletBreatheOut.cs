using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBulletBreatheOut : Bullet
    {
        Vector2 originPos;
        Vector2 newPos;
        float originalOrient;
        Vector2 directionAway;
        double timeAlive;
        public FinalBossBulletBreatheOut(Vector2 position, float orientation) : base(position)
        {
            this.sprite = Art.FinalBossBullet;

            this.Position = position;
            this.originPos = position;
            this.originalOrient = orientation;
            this.Orientation = MathHelper.ToRadians(orientation);
            this.Velocity = new Vector2(2, 2);
            Vector2 direction = new Vector2((float)Math.Cos(Orientation), (float)Math.Sin(Orientation));
            direction.Normalize();
            this.Position += direction * Velocity;
            this.isEnemy = true;
            this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));

        }

        public override void Update(GameTime gt)
        {

            timeAlive += gt.ElapsedGameTime.TotalSeconds;

            if (originalOrient >= 0)
            { newPos = RotateAboutOrigin(Position, originPos, .0075f); }
            else
            { newPos = RotateAboutOrigin(Position, originPos, -.0075f); }
            Position = newPos;
            directionAway = originPos - Position;
            directionAway.Normalize();
            if (timeAlive < 1)
            { Position -= directionAway * 1.5f; }
            else if (timeAlive < 1.75)
            { Position += directionAway * 2f; }
            Position -= directionAway * 1.5f;

            // remove the bullet if it goes out of bounds
            if (Vector2.Distance(Position, originPos) > 800)
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

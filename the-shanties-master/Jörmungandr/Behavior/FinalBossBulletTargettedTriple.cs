using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets
{
    class FinalBossBulletTargettedTriple : Bullet
    {
        Vector2 direction = Player.Instance.Position;
        float originalOrient;

        public FinalBossBulletTargettedTriple(Vector2 position, float orientation) : base(position)
        {
            this.sprite = Art.FinalBossBullet;

            this.Position = position;
            originalOrient = orientation;
            this.Orientation = (float)(Math.Atan2(Player.Instance.Position.Y - Position.Y, Player.Instance.Position.X - Position.X));
            this.Velocity = new Vector2(4f, 4f);

            if (originalOrient == 220)
            { this.direction = Player.Instance.Position - Position + new Vector2((float)Math.Cos(210), (float)Math.Sin(210)); }
            else if (originalOrient == 330)
            { this.direction = Player.Instance.Position - Position + new Vector2((float)Math.Cos(330), (float)Math.Sin(330)); }
            else
            { this.direction = Player.Instance.Position - Position; }
            direction.Normalize();

        }

        public override void Update(GameTime gt)
        {

            this.Position += direction * Velocity;

            // remove the bullet if it goes out of bounds
            if (Position.Y > 800 || Position.Y < -200 || Position.X > 800 || Position.X < -200)
            {
                this.Expire();
            }
        }
    }
}

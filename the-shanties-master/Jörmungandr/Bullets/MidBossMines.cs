using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr.Bullets.Factory;
using Jörmungandr.Bullets;

namespace Jörmungandr
{
    public class MidBossMines : Enemy
    {
        Vector2 ScreenSize = new Vector2(600, 800);
        double shootTime = 0;
        public static Random rand = new Random();
        Vector2 originPos;
        Vector2 directionAway;

        public MidBossMines(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
            this.sprite = image;
            this.Position = position;
        }


        public override void Update(GameTime gt)
        {
            //Position += Velocity;
            Vector2 newPos = RotateAboutOrigin(Position, originPos, .075f);
            Position = newPos;
            directionAway = originPos - Position;
            directionAway.Normalize();
            Position -= directionAway;

            Velocity *= 1.0f;
            this.shootTime += gt.ElapsedGameTime.TotalSeconds;
            base.Update(gt);
            if (shootTime > .15)
            {
                EntityManager.Add(bulletFactory.CreateBullet(this.Position, 90));
                EntityManager.Add(bulletFactory.CreateBullet(this.Position, 105));
                EntityManager.Add(bulletFactory.CreateBullet(this.Position, 120));
                EntityManager.Add(bulletFactory.CreateBullet(this.Position, 135));
                shootTime = 0;
            }

            // remove the bullet if it goes out of bounds
            if (Vector2.Distance(Position, originPos) > 100)
            {
                this.Expire();
            }
        }

        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }

        // should not be called
        protected override void Fire()
        {
            throw new NotImplementedException();
        }

        //#endregion

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr.Bullets.Factory;
using Jörmungandr.Bullets;

namespace Jörmungandr
{
    public class FinalBossSpiralTriple : Enemy
    {
        Vector2 ScreenSize = new Vector2(600, 800);
        double shootTime = 0;
        public static Random rand = new Random();
        Vector2 originPos;
        Vector2 directionAway;

        public FinalBossSpiralTriple(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
            this.sprite = image;
            this.Position = position;

            originPos = new Vector2(position.X + 1, position.Y);
        }

        public override void Update(GameTime gt)
        {
            Vector2 newPos = RotateAboutOrigin(Position, originPos, .08f);
            Position = newPos;
            directionAway = originPos - Position;
            directionAway.Normalize();
            Position -= directionAway * 1.25f;

            //Velocity *= 1.0f;
            this.shootTime += gt.ElapsedGameTime.TotalSeconds;
            base.Update(gt);
            if (shootTime > .05)
            {
                EntityManager.Add(new FinalBossBulletTargettedTriple(Position, 90));
                EntityManager.Add(new FinalBossBulletTargettedTriple(Position, 210));
                EntityManager.Add(new FinalBossBulletTargettedTriple(Position, 330));
                shootTime = 0;
            }

            // remove the bullet if it goes out of bounds
            if (Vector2.Distance(Position, originPos) > 175)
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

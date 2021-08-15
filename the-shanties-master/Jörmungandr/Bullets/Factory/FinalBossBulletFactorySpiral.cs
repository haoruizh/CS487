using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets.Factory
{
    public class FinalBossBulletFactorySpiral : AbstractBulletFactory
    {
        Vector2 spiral = new Vector2(-1, 1);
        double shootTime;
        public FinalBossBulletFactorySpiral(Vector2 position)
        {
            this.sprite = Art.Jellyfish;
            this.Position = position;
            this.Orientation = MathHelper.ToRadians(90);

            //this.Velocity = new Vector2(2, 2);
            //this.hitbox = new Rectangle((int)this.Position.X, (int)(this.Position.Y), (int)(this.Size.X), (int)(this.Size.Y));
        }

        // passing in orientation will not do anything hmm.. not sure if this should be an enemy factory or bullet factory
        public override Bullet CreateBullet(Vector2 position, float orientation)
        {
            return new FinalBossBullet(position, orientation);
        }

        //public Bullet CreateBullet(Vector2 position)
        //{
        //    return new Mine(position);
        //}

        public override void Update(GameTime gt)
        {
            shootTime += gt.ElapsedGameTime.TotalSeconds;
            if (Math.Floor(shootTime) == 5)
            {
            }
            //Position += Spiral;
            //throw new NotImplementedException();
        }
    }
}

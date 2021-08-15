using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets.Factory
{
    public class EnemyBulletFactory : AbstractBulletFactory
    {
        public override Bullet CreateBullet(Vector2 position, float orientation)
        {
            return new EnemyBullet(position, orientation);
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

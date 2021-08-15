using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets.Factory
{
    public class PlayerPowerBulletFactory : AbstractBulletFactory
    {
        public override Bullet CreateBullet(Vector2 position, float orientation)
        {
            return new PlayerPowerBullet(position, orientation);
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

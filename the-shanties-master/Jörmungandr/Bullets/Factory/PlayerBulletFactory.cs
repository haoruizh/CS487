using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Bullets.Factory
{
    /// <summary>
    /// Creates a player bullet. Should be instantiated in the player class.
    /// </summary>
    public class PlayerBulletFactory : AbstractBulletFactory
    {
        public override Bullet CreateBullet(Vector2 position, float orientation)
        {
            return new PlayerBullet(position, orientation);
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;

namespace Jörmungandr.Bullets.Factory
{
    public abstract class AbstractBulletFactory : Entity
    {
        public abstract Bullet CreateBullet(Vector2 position, float orientation);
        
        // TODO: Add update function for this class
    }
}

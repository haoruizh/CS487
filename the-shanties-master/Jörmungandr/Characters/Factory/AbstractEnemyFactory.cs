using Microsoft.Xna.Framework;

namespace Jörmungandr.Characters.Factory
{
    /// <summary>
    /// Base abstract factory for other enemy factories to aggregate from
    /// </summary>
    public abstract class AbstractEnemyFactory
    {
        public abstract Enemy CreateEnemy(Vector2 position, int points, int fireRate, int health);
    }
}

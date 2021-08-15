using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Characters.Factory
{
    public class MidBossFactory : AbstractEnemyFactory
    {
        public override Enemy CreateEnemy(Vector2 position, int points, int fireRate, int health)
        {
            return new MidBoss(new EnemyBulletFactory(), Art.MidBoss, position, points, fireRate, health);
        }
    }
}

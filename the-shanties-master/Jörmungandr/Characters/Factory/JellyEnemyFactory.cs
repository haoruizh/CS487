using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Jörmungandr.Bullets.Factory;

namespace Jörmungandr.Characters.Factory
{
    public class JellyEnemyFactory : AbstractEnemyFactory
    {
        public override Enemy CreateEnemy(Vector2 position, int points, int fireRate, int health)
        {
            return new JellyEnemy(new EnemyBulletFactory(), Art.Jellyfish, position, points, fireRate, health);
        }
    }
}

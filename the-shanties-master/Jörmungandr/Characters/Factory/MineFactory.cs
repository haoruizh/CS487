using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Characters.Factory
{
    public class MineFactory : AbstractEnemyFactory
    { 
        public override Enemy CreateEnemy(Vector2 position, int points, int fireRate, int health)
        {
            return new Mine(new EnemyBulletFactory(), Art.Mine, position, points, fireRate, health);
        }
    }
}

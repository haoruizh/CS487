using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jörmungandr.Characters.Factory
{
    public class RockEnemyFactory : AbstractEnemyFactory
    {
        public override Enemy CreateEnemy(Vector2 position, int points, int fireRate, int health)
        {
            return new RockEnemy(null, Art.Rock, position, points, fireRate, health);
        }
    }
}

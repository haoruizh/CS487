using Jörmungandr.Bullets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Collison
{
    public class PlayerBulletEnemyBulletCollision:CollisonInterface
    {

        private List<Bullet> playerBullet;
        private Bullet enemyBullet;
        public PlayerBulletEnemyBulletCollision(List<Bullet> playerBullet, Bullet enemy)
        {
            this.playerBullet = playerBullet;
            this.enemyBullet = enemy;
        }

        public int CollisionDetect()
        {
            for (int index = 0; index < playerBullet.Count; index++)
            {
                // detect if any collision between target enemy bullet and player bullet
                if (!this.playerBullet[index].IsEnemy() && this.playerBullet[index].IsHit(this.enemyBullet))
                {
                    // if they collided, return the index of the player bullet
                    return index;
                }
            }
            // if not, return -1
            return -1;
        }

        public void UpdateBullets(List<Bullet> newBullets)
        {
            this.playerBullet = newBullets;
        }

        public void UpdateEnemyBullet(Bullet newEnemy)
        {
            this.enemyBullet = newEnemy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr.Bullets;

namespace Jörmungandr.Collision
{
    public class PlayerEnemyBulletCollision : CollisonInterface
    {
        private List<Bullet> enemyBullet;
        private Player player;
        public PlayerEnemyBulletCollision(List<Bullet> enemiesBullet, Player player)
        {
            this.enemyBullet = enemiesBullet;
            this.player = player;
        }

        public int CollisionDetect()
        {
            for (int index = 0; index < enemyBullet.Count; index++)
            {

                // detect if any collision between target enemy and player
                if (this.enemyBullet[index].IsEnemy() && this.player.IsHit(this.enemyBullet[index]) )
                {
                    // if they collided, return the index of the enemy
                    return index;
                }
            }
            // if not, return -1
            return -1;
        }

        public void UpdateBullets(List<Bullet> newBullets)
        {
            this.enemyBullet = newBullets;
        }

        public void UpdatePlayer(Player newPlayer)
        {
            this.player = newPlayer;
        }
    }
}

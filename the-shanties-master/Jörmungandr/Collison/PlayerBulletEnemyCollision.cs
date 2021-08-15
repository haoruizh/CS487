using Jörmungandr.Bullets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Collision
{
    public class PlayerBulletEnemyCollision : CollisonInterface
    {
        private List<Bullet> playerBullet;
        private Enemy enemy;
        public PlayerBulletEnemyCollision(List<Bullet> playerBullet, Enemy enemy)
        {
            this.playerBullet = playerBullet;
            this.enemy = enemy;
        }

        public int CollisionDetect()
        {
            for (int index = 0; index < playerBullet.Count; index++)
            {
                // detect if any collision between target enemy and player bullet
                if (!this.playerBullet[index].IsEnemy() && this.enemy.IsHit(this.playerBullet[index]))
                {
                    Player.Instance.UpdateScore(enemy.PointValue);
                    // if they collided, return the index of the enemy
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

        public void UpdateEnemy(Enemy newEnemy)
        {
            this.enemy = newEnemy;
        }
    }
}

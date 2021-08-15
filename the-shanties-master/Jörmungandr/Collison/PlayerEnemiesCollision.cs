using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr;

namespace Jörmungandr.Collision
{
    public class PlayerEnemiesCollision : CollisonInterface
    {
        private List<Enemy> enemies;
        private Player player;
        public PlayerEnemiesCollision(List<Enemy> enemies, Player player)
        {
            this.enemies = enemies;
            this.player = player;
        }

        public int CollisionDetect()
        {
            for (int index = 0; index < enemies.Count; index++)
            {
                // detect if any collision between target enemy and player
                if (this.player.IsCollideWithCharacter(this.enemies[index]))
                {
                    // if they collided, return the index of the enemy
                    return index;
                }
            }
            // if not, return -1
            return -1;
        }

        public void UpdateEnemies(List<Enemy> newEnemies)
        {
            this.enemies = newEnemies;
        }

        public void UpdatePlayer(Player newPlayer)
        {
            this.player = newPlayer;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Jörmungandr.Bullets;
using Jörmungandr.Collision;
using Jörmungandr.Characters;
using Jörmungandr.Collison;

namespace Jörmungandr
{
    static class EntityManager
    {
        private static CollisionManager collisionManager = new CollisionManager();
        static List<Entity> entities = new List<Entity>();
        static List<Enemy> enemies = new List<Enemy>();
        static List<Bullet> playerBullets = new List<Bullet>();
        static List<Bullet> enemyBullets = new List<Bullet>();
        static List<Entity> addedEntities = new List<Entity>();
        static List<Item.PlayerItem> playerDropItems = new List<Item.PlayerItem>();
        static Player player = Player.Instance;
        static PlayerEnemiesCollision playerEnemiesCollisionCmd = new PlayerEnemiesCollision(enemies, player);
        static PlayerEnemyBulletCollision playerEnemyBulletCollisionCmd = new PlayerEnemyBulletCollision(enemyBullets, player);
        static PlayerBulletEnemyCollision playerBulletEnemyCollisionCmd = new PlayerBulletEnemyCollision(playerBullets, null);
        static PlayerBulletEnemyBulletCollision playerBulletEnemyBulletCollsionCmd = new PlayerBulletEnemyBulletCollision(playerBullets, null);
        static PlayerItemCollision playerItemCollisionCmd = new PlayerItemCollision(player, playerDropItems);


        static bool isUpdating;

        public static int Count => entities.Count;
        public static int EnemyCount => enemies.Count;

        /// <summary>
        /// check if it is valid to add entity into entity lists, and add new entity to corresponding list.
        /// </summary>
        /// <param name="entity">the new entity</param>
        public static void Add(Entity entity)
        {
            if (!isUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        /// <summary>
        /// Add new entity to the manager's entity list..
        /// </summary>
        /// <param name="entity">the new entity.</param>
        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);
            // Add (if (entity is bullet) first when implementing bullets
            if (entity is Enemy)
                enemies.Add(entity as Enemy);
            else if (entity is Bullet)
                if ((entity as Bullet).IsEnemy())
                {
                    enemyBullets.Add(entity as Bullet);
                }
                else
                {
                    playerBullets.Add(entity as Bullet);
                }
            else if  (entity is Item.PlayerItem)
            {
                playerDropItems.Add(entity as Item.PlayerItem);
            }
        }

        /// <summary>
        /// run player bullet enemy bullet collision detection and do corresponding action.
        /// </summary>
        private static void PlayerBulletEnemyBulletCollisionDetection()
        {
            playerBulletEnemyBulletCollsionCmd.UpdateBullets(playerBullets);
            foreach (Bullet enemy in enemyBullets)
            {
                playerBulletEnemyBulletCollsionCmd.UpdateEnemyBullet(enemy);
                collisionManager.SetCommand(playerBulletEnemyBulletCollsionCmd);
                int colledIndex = collisionManager.collisionDetection();
                if (colledIndex != -1)
                {
                    if(playerBullets[colledIndex].CanKillBullets())
                    {
                        playerBullets[colledIndex].Expire();
                        enemy.Expire();
                    }
                }
            }
        }

        /// <summary>
        /// run player enemy bullet collision detection and do corresponding action.
        /// </summary>
        private static void PlayerEnemyBulletCollisionDetection()
        {
            playerEnemyBulletCollisionCmd.UpdatePlayer(player);
            playerEnemyBulletCollisionCmd.UpdateBullets(enemyBullets);
            collisionManager.SetCommand(playerEnemyBulletCollisionCmd);
            int colledIndex = collisionManager.collisionDetection();
            if (colledIndex != -1)
            {
                enemyBullets[colledIndex].Expire();
                // player re-spawn
                player.LoseLife();
                player.SetRespawnPosition();
            }
        }

        /// <summary>
        /// run player item collision detection and do corresponding action.
        /// </summary>
        private static void PlayerItemCollisionDetection()
        {
            playerItemCollisionCmd.UpdatePlayer(player);
            playerItemCollisionCmd.UpdateItemList(playerDropItems);
            collisionManager.SetCommand(playerItemCollisionCmd);
            int colledIndex = collisionManager.collisionDetection();
            if(colledIndex != -1)
            {
                playerDropItems[colledIndex].DoAffect(player);
                playerDropItems[colledIndex].Expire();
            }

        }

        /// <summary>
        /// run player enemy collision detection and do corresponding action.
        /// </summary>
        private static void PlayerEnemiesCollisionDetection()
        {
            playerEnemiesCollisionCmd.UpdateEnemies(enemies);
            playerEnemiesCollisionCmd.UpdatePlayer(player);
            collisionManager.SetCommand(playerEnemiesCollisionCmd);
            int colledIndex = collisionManager.collisionDetection();
            if (colledIndex != -1)
            {
                enemies[colledIndex].LoseLife();
                // player re-spawn
                player.LoseLife();
                player.SetRespawnPosition();
            }
        }

        /// <summary>
        /// Run player and enemy collision commands.
        /// </summary>
        private static void PlayerEnemyCollision()
        {
            PlayerEnemiesCollisionDetection();
            PlayerEnemyBulletCollisionDetection();
        }

        /// <summary>
        /// run player bullet enemy detection and do corresponding action.
        /// </summary>
        private static void PlayerBulletEnemyCollsionDetection()
        {
            playerBulletEnemyCollisionCmd.UpdateBullets(playerBullets);
            foreach (Enemy enemy in enemies)
            {
                playerBulletEnemyCollisionCmd.UpdateEnemy(enemy);
                collisionManager.SetCommand(playerBulletEnemyCollisionCmd);
                int colledIndex = collisionManager.collisionDetection();
                if (colledIndex != -1)
                {
                    playerBullets[colledIndex].Expire();
                    if (enemy is Mine)
                        enemy.didExplode = true;
                    enemy.LoseLife();
                }
            }
        }

        /// <summary>
        /// Run update on entities 
        /// </summary>
        /// <param name="gt">Gametime</param>
        public static void Update(GameTime gt)
        {
            isUpdating = true;

            // handle collision
            // handle player enemy collision
            if (!player.IsExpired)
            {
                PlayerEnemiesCollisionDetection();
                PlayerEnemyBulletCollisionDetection();
            }

            // handle player bullet enemy collision
            PlayerBulletEnemyCollsionDetection();
            // handle player bullet and enemy bullet collision
            PlayerBulletEnemyBulletCollisionDetection();
            // handle player item collision
            PlayerItemCollisionDetection();

            // Update current entities
            foreach (var entity in entities)
                entity.Update(gt);

            isUpdating = false;
            // Add any potential new entities
            foreach (var entity in addedEntities)
                AddEntity(entity);

            addedEntities.Clear();

            // update all entities.
            entities = entities.Where(x => !x.IsExpired).ToList();
            enemies = enemies.Where(x => !x.IsExpired).ToList();
            playerBullets = playerBullets.Where(x => !x.IsExpired).ToList();
            enemyBullets = enemyBullets.Where(x => !x.IsExpired).ToList();
            playerDropItems = playerDropItems.Where(x => !x.IsExpired).ToList();
        }

        /// <summary>
        /// Draw all entities that is not expired
        /// </summary>
        /// <param name="spriteBatch">Sprite drawer.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}

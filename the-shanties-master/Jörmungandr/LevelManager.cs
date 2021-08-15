using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Jörmungandr.Bullets;
using Jörmungandr.Characters.Factory;
using Microsoft.Xna.Framework;
using Jörmungandr.Item;
using Jörmungandr.Item.Factory;
using Newtonsoft.Json;
using System.IO;
using Jörmungandr.Behavior;

namespace Jörmungandr.Level
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelManager
    {
        #region jsonDataModels
        public class Phase
        { 
            public List<EnemyData> enemies { get; set; }
            public TimeInterval time { get; set; }
        }

        public class EnemyData
        {
            public string enemy { get; set; }
            public string behavior { get; set; }
            public string position { get; set; }
            public int health { get; set; } 
            public int amount { get; set; }
            public int points { get; set; }
            public int interval { get; set; }
            public int fireRate { get; set; }
            public BulletData bulletType { get; set; }
        }

        public class BulletData
        {
            public string type { get; set; }
            public int speed { get; set; }
        }

        public class TimeInterval
        {
            public int start { get; set; }
            public int end { get; set; }
        }
        #endregion

        #region factories
        private JellyEnemyFactory jellyFactory = new JellyEnemyFactory();
        private RockEnemyFactory rockFactory = new RockEnemyFactory();
        private MidBossFactory midBossFactory = new MidBossFactory();
        private FinalBossFactory finalBossFactory = new FinalBossFactory();
        private PlayerHealItemFactory playerHealItemFactory = new PlayerHealItemFactory();
        private PlayerBoostItemFactory playerBoostItemFactory = new PlayerBoostItemFactory();
        private BehaviorFactory behaviorFactory = new BehaviorFactory();

        #endregion

        Random rand = new Random();

        // this ensures the bosses are only spawned once
        bool finalBossSpawned = false;
        bool midBossSpawned = false;
        TimeSpan itemTimeSpan = TimeSpan.FromSeconds(5);

        private Dictionary<string, Phase> phases;
        private Dictionary<string, TimeSpan> spawnChances;

        public bool isMiddBossDead = false;

        public LevelManager()
        {
            // Read in level information from JSON

            string filePath = System.IO.Path.GetFullPath("Phases.json");
            StreamReader r = new StreamReader(@"C:\Users\Allison\source\repos\the-shanties\Jörmungandr\JSON\Phases.json"); // C:\gitRepos\Jormungandr\the-shanties\Jörmungandr\JSON
            phases = JsonConvert.DeserializeObject<Dictionary<string, Phase>>(r.ReadToEnd());

            // fill in the spawnchances (so we can keep track of the intervals and change them without changing the phase info)
            spawnChances = new Dictionary<string, TimeSpan>();
            foreach (string k in phases.Keys)
            {
                foreach (EnemyData data in phases[k].enemies)
                {
                    if (!spawnChances.ContainsKey(data.enemy))
                        spawnChances.Add(data.enemy, TimeSpan.FromSeconds(data.interval)); 
                }
            }
        }

        /// <summary>
        /// Loads the level based on the phase number time.
        /// </summary>
        /// <param name="gt"></param>
        public void LoadLevel(GameTime gt)
        {
            if (gt.TotalGameTime.TotalSeconds <= phases["Phase1"].time.end)
            {
                this.HandlePhase(gt, "Phase1");
            }
            else if (gt.TotalGameTime.TotalSeconds <= phases["Phase2"].time.end)
            {
                if (!isMiddBossDead)
                {
                    this.HandlePhase(gt, "Phase2");

                    if (itemTimeSpan <= TimeSpan.Zero)
                    {
                        EntityManager.Add(this.playerHealItemFactory.CreatePlayerItem(GetRandomSpawnPositionTop()));
                        this.itemTimeSpan = TimeSpan.FromSeconds(7);
                    }
                    else
                        this.itemTimeSpan -= gt.ElapsedGameTime;
                }
                else
                {
                    this.HandlePhase(gt, "Phase3");
                    InputHandler.InvertControls();
                }
            }
            else if (gt.TotalGameTime.TotalSeconds <= phases["Phase3"].time.end)
            {
                this.HandlePhase(gt, "Phase3");
                InputHandler.InvertControls();
            }
            else if (gt.TotalGameTime.TotalSeconds <= phases["Phase4"].time.end)
            {
                if (itemTimeSpan <= TimeSpan.Zero)
                {
                    EntityManager.Add(this.playerHealItemFactory.CreatePlayerItem(GetRandomSpawnPositionTop()));
                    this.itemTimeSpan = TimeSpan.FromSeconds(7);
                }
                else
                    this.itemTimeSpan -= gt.ElapsedGameTime;

                this.HandlePhase(gt, "Phase4");
            }
        }

        /// <summary>
        /// Spawns enemies from a given phase.
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="phaseName"></param>
        private void HandlePhase(GameTime gt, string phaseName)
        {
            Phase phase = this.phases[phaseName];
            // loop through the enemies
            foreach (EnemyData enemyData in phase.enemies)
            {
                // check if the enemy should spawn
                if (this.spawnChances[enemyData.enemy] <= TimeSpan.Zero)
                {
                    // spawn the enemies
                    for (int j = 0; j < enemyData.amount; j++)
                    {
                        Entity newEntity = this.CreateEnemyFromData(enemyData);
                       
                        if (newEntity != null)
                        {
                            newEntity.AddBehavior(this.behaviorFactory.CreateBehavior(enemyData.behavior));
                            EntityManager.Add(newEntity);
                            this.spawnChances[enemyData.enemy] = TimeSpan.FromSeconds(enemyData.interval);
                        }
                    }
                }
                else
                {
                    // decrease spawn chance time
                    this.spawnChances[enemyData.enemy] -= gt.ElapsedGameTime;
                }
            }
        }

        /// <summary>
        /// Creates the enemies from the json information
        /// </summary>
        /// <param name="enemyType"></param>
        /// <param name="positionType"></param>
        /// <returns></returns>
        private Enemy CreateEnemyFromData(EnemyData data)
        {
            // Determine its spawnPosition
            Vector2 spawnPosition = Vector2.Zero;
            if (data.position == "top")
                spawnPosition = this.GetRandomSpawnPositionTop();
            else if (data.position == "left")
                spawnPosition = this.GetRandomSpawnPositionLeft();
            else if (data.position == "centerTop")
                spawnPosition = new Vector2(300, 90);

            // then create the enemy
            switch (data.enemy)
            {
                case "rockEnemy":
                    return this.rockFactory.CreateEnemy(spawnPosition, data.points, data.fireRate, data.health);
                case "jellyEnemy":
                    return this.jellyFactory.CreateEnemy(spawnPosition, data.points, data.fireRate, data.health);
                case "midBoss":
                    if (!midBossSpawned)
                    {
                        midBossSpawned = true;
                        return this.midBossFactory.CreateEnemy(spawnPosition, data.points, data.fireRate, data.health);
                    }
                    break;
                case "finalBoss":
                    if (!finalBossSpawned)
                    {
                        finalBossSpawned = true;
                        InputHandler.UnInvertControls();
                        return this.finalBossFactory.CreateEnemy(spawnPosition, data.points, data.fireRate, data.health);
                    }
                    break;
            }

            return null;
        }


        #region randomPositionGenerators
        private Vector2 GetRandomSpawnPositionTop()
        {
            return new Vector2(rand.Next(0, 600), 0);
        }

        private Vector2 GetRandomSpawnPositionLeft()
        {
            return new Vector2(-30, rand.Next(0, 400));
        }

        #endregion
    }
}

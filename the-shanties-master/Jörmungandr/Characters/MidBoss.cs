using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Jörmungandr.Bullets;
using Jörmungandr.Bullets.Factory;
using Jörmungandr.Characters.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Jörmungandr
{
    internal class MidBoss : Enemy
    {
        public List<IEnumerator<int>> bulletPatterns = new List<IEnumerator<int>>();
        Vector2 ScreenSize = new Vector2(600, 800);
        public static Random rand = new Random();
        public static double angleModBasic = 0;
        public static double angleModFiveBurst = 0;
        public static double angleModCurveBurst = 0;
        public static double angleModShrapnel = 0;
        public static int angleModSwitchBasic = 0;
        public static int angleModSwitchShrapnel = 0;
        public static int bulletPattern = 0;
        public static double timeAlive = 0;

        public static double timeLastShotFiveBurst = 0;
        public static double timeLastShotCurveBurst = 0;
        public static double timeLastShotShrapnel = 0;

        public int createdMines = 0;

        private MineFactory MineFactory = new MineFactory();

        public MidBoss(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
            this.sprite = image;
        }

        public override void Update(GameTime gt)
        {
            if (this.timeUntilStart <= 0)
            {
                ApplyBehaviors();
                ApplyBulletPatterns();
            }
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }

            Position += Velocity;
            Velocity *= .3f;

            if (Position.Y >= 830 || this.IsExpired)
            {
                this.Expire();
            }
            // if it is in proximity of the player, collision
            if (Vector2.Distance(this.Position, Player.Instance.Position) <= 30)
            {
                this.Expire();
            }

            if (Math.Floor(timeAlive) == 1)
            {
                if (!bulletPatterns.Contains((IEnumerator<int>)MidBossBulletPatternMines()))
                { this.AddBulletPattern(MidBossBulletPatternMines()); }
            }

            if (timeLastShot > 0)
            {
                timeLastShot -= gt.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // fire the shots
                this.Fire();
                timeLastShot = 3;
            }

            Velocity *= 1;
        }

        #region Behaviors
        // Adds a BulletPattern
        protected void AddBulletPattern(IEnumerable<int> bulletPattern)
        {
            if (!bulletPatterns.Contains((IEnumerator<int>)bulletPattern))
            {
                bulletPatterns.Add(bulletPattern.GetEnumerator());
            }
        }

        // Remove a BulletPattern - Unsure it working, stick to Clear
        protected void RemoveBulletPattern(IEnumerable<int> bulletPattern)
        {
            if (bulletPatterns.Contains((IEnumerator<int>)bulletPattern))
            {
                bulletPatterns.Remove(bulletPattern.GetEnumerator());
            }
        }

        // Clear BulletPatterns
        protected void ClearBulletPatterns()
        {

            bulletPatterns.Clear();
        }

        // Applys a BulletPattern
        protected void ApplyBulletPatterns()
        {
            for (int i = 0; i < bulletPatterns.Count; i++)
            {
                if (!bulletPatterns[i].MoveNext())
                    bulletPatterns.RemoveAt(i--);
            }
        }

        // Adde Bullet Pattern
        IEnumerable<int> MidBossBulletPatternMines()
        {
            while (true)
            {
                if (createdMines == 0)
                {
                    EntityManager.Add(new MidBossMines(bulletFactory, Art.Mine, new Vector2(Position.X - 1, Position.Y), 0, 0, 0));
                    createdMines = 1;
                }
                yield return 0;
            }
        }

        // Fire mines
        protected override void Fire()
        {
            EntityManager.Add(this.MineFactory.CreateEnemy(this.Position, 10, 0, 1));
        }


        protected void updateAngleMod()
        {
            if (angleModSwitchBasic == 0)
            {
                angleModBasic += 3;
                if (angleModBasic == 30)
                    angleModSwitchBasic = 1;
            }
            else if (angleModSwitchBasic == 1)
            {
                angleModBasic -= 3;
                if (angleModBasic == -30)
                    angleModSwitchBasic = 0;
            }
        }

        protected void updateAngleModFiveBurst()
        {
            angleModFiveBurst -= 15;
        }

        protected void updateAngleModCurveBurst()
        {
            angleModCurveBurst += 40;
        }

        protected void updateAngleModShrapnel()
        {
            if (angleModSwitchShrapnel == 0)
            {
                angleModShrapnel += 15;
                if (angleModShrapnel == 180)
                    angleModSwitchShrapnel = 1;
            }
            else if (angleModSwitchShrapnel == 1)
            {
                angleModShrapnel -= 15;
                if (angleModShrapnel == 0)
                    angleModSwitchShrapnel = 0;
            }
        }

        public Vector2 getPos()
        {
            return Position;
        }
        #endregion

        public override void Expire()
        {
            base.Expire();
            Game.gameInstance.levelManager.isMiddBossDead = true;
        }
    }
}
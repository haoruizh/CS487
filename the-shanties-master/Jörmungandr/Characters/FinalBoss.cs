using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Jörmungandr.Bullets;
using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Jörmungandr
{
    internal class FinalBoss : Enemy
    {
        public List<IEnumerator<int>> bulletPatterns = new List<IEnumerator<int>>();
        //Vector2 ScreenSize = new Vector2(600, 800);
        //public static Random rand = new Random();


        public static double angleModBasic = 0;
        public static double angleModFiveBurst = 0;
        public static double angleModCurveBurst = 0;
        public static double angleModShrapnel = 0;
        public static int angleModSwitchBasic = 0;
        public static int angleModSwitchShrapnel = 0;
        public static double timeLastShotShrapnel = 0;
        public static double timeLastShotFiveBurst = 0;
        private int shotCounterFiveBurst = 0;
        public static double timeLastShotCurveBurst = 0;
        //public static int bulletPattern = 0;
        public static double timeAlive = 0;
        private int shotCounterBasic = 0;

        //Begin fields that matter
        public static double timeLastShotSpiral = 0;
        public static double timeLastShotSpinOut = 0;
        public static double directionToSpinOut = 360;
        public static double timeLastShotBreatheOut = 0;
        public static double directionToBreatheOut = 360;
        public static double timeLastShotSpiralTriple = 0;
        //End fields that matter
        public FinalBoss(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
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

            if (timeLastShot > 0)
            {
                timeLastShot -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotSpiral > 0) //Shot timer for Spiral attack, matters
            {
                timeLastShotSpiral -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotSpinOut > 0) //Shot timer for SpinOut attack, matters
            {
                timeLastShotSpinOut -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotBreatheOut > 0) //Shot timer for BreatheOut attack, matters
            {
                timeLastShotBreatheOut -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotSpiralTriple > 0) //Shot timer for SpiralTriple attack, matters
            {
                timeLastShotSpiralTriple -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotFiveBurst > 0)
            {
                timeLastShotFiveBurst -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotCurveBurst > 0)
            {
                timeLastShotCurveBurst -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (timeLastShotShrapnel > 0)
            {
                timeLastShotShrapnel -= gt.ElapsedGameTime.TotalSeconds;
            }

            timeAlive += gt.ElapsedGameTime.TotalSeconds;

            if (Math.Floor(timeAlive) == 0) //First AttackPattern of First attack phase of FinalBoss
            {
                if (!bulletPatterns.Contains((IEnumerator<int>)FinalBossBulletPatternSpiral()))
                { this.AddBulletPattern(FinalBossBulletPatternSpiral()); }
            }
            if (Math.Floor(timeAlive) == 2) //Second AttackPattern of First attack phase of FinalBoss
            {
                if (!bulletPatterns.Contains((IEnumerator<int>)FinalBossBulletPatternSpinOut()))
                { this.AddBulletPattern(FinalBossBulletPatternSpinOut()); }
            }
            if (Math.Floor(timeAlive) == 46) //End first attack phase of FinalBoss
            {
                this.ClearBulletPatterns();
            }
            if (Math.Floor(timeAlive) == 49) //First AttackPattern of Second attack phase of FinalBoss
            {
                if (!bulletPatterns.Contains((IEnumerator<int>)FinalBossBulletPatternBreatheOut()))
                { this.AddBulletPattern(FinalBossBulletPatternBreatheOut()); }
            }
            if (Math.Floor(timeAlive) == 51) //Second AttackPattern of Second attack phase of FinalBoss
            {
                if (!bulletPatterns.Contains((IEnumerator<int>)FinalBossBulletPatternSpiralTriple()))
                { this.AddBulletPattern(FinalBossBulletPatternSpiralTriple()); }
            }
        }
        #region Behaviors
        IEnumerable<int> FinalBossBehavior()
        {
            Orientation = 0;
            Velocity = new Vector2(0, 0);
            float posX, posY;
            int xFlip = 0;
            int yFlip = 0;
            while (true)
            {
                posY = Position.Y;
                posX = Position.X;

                if (xFlip == 0)
                {
                    Position.X += (float)(.25);
                    if (Position.X >= 350)
                    { xFlip = 1; }
                }
                else
                {
                    Position.X -= (float)(.25);
                    if (Position.X <= 250)
                    { xFlip = 0; }
                }
                if (yFlip == 0)
                {
                    Position.Y += (float)(.15);
                    if (Position.Y >= 115)
                    { yFlip = 1; }
                }
                else
                {
                    Position.Y -= (float)(.15);
                    if (Position.Y <= 90)
                    { yFlip = 0; }
                }
                yield return 0;
            }

        }

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

        //Begin BulletPatterns from video 
        IEnumerable<int> FinalBossBulletPatternSpiral()
        {

            while (true)
            {
                if (timeLastShotSpiral <= 0)
                {
                    EntityManager.Add(new FinalBossSpiral(bulletFactory, Art.FloatingCannonLeft, new Vector2(Position.X - 1, Position.Y), 0, 0, 0));
                    EntityManager.Add(new FinalBossSpiral(bulletFactory, Art.FloatingCannonRight, new Vector2(Position.X + 1, Position.Y), 0, 0, 0));
                    timeLastShotSpiral = 5;
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternSpinOut()
        {
            while (true)
            {
                if (timeLastShotSpinOut <= 0)
                {
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 0f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 22.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 45f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 67.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 90f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 112.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 135f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 157.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 180f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 202.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 225f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 247.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 270f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 292.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 315f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletSpinOut(this.Position, 337.5f + (float)directionToSpinOut));
                    if (directionToSpinOut == 360)
                    {
                        timeLastShotSpinOut = 2;
                        directionToSpinOut = -360;
                    }
                    else
                    {
                        timeLastShotSpinOut = 4.5;
                        directionToSpinOut = 360;
                    }
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternBreatheOut()
        {
            while (true)
            {
                if (timeLastShotBreatheOut <= 0)
                {
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 0f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 22.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 45f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 67.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 90f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 112.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 135f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 157.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 180f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 202.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 225f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 247.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 270f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 292.5f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 315f + (float)directionToSpinOut));
                    EntityManager.Add(new FinalBossBulletBreatheOut(this.Position, 337.5f + (float)directionToSpinOut));
                    if (directionToBreatheOut == 360)
                    {
                        timeLastShotBreatheOut = 2;
                        directionToBreatheOut = -360;
                    }
                    else
                    {
                        timeLastShotBreatheOut = 4.5;
                        directionToBreatheOut = 360;
                    }
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternSpiralTriple()
        {

            while (true)
            {
                if (timeLastShotSpiralTriple <= 0)
                {
                    EntityManager.Add(new FinalBossSpiralTriple(bulletFactory, Art.FloatingCannonLeft, new Vector2(Position.X - 1, Position.Y), 0, 0, 0));
                    timeLastShotSpiralTriple = 5;
                }
                yield return 0;
            }
        }
        //End BulletPatterns from video

        IEnumerable<int> FinalBossBulletPattern1()
        {
            while (true)
            {

                if (timeLastShot <= 0)
                {
                    if (shotCounterBasic % 3 == 0)
                    { EntityManager.Add(new FinalBossBullet(Position, 60 + (float)angleModBasic)); }
                    else if (shotCounterBasic % 3 == 1)
                    { EntityManager.Add(new FinalBossBullet(Position, 90 + (float)angleModBasic)); }
                    else if (shotCounterBasic % 3 == 2)
                    { EntityManager.Add(new FinalBossBullet(Position, 120 + (float)angleModBasic)); }
                    timeLastShot = .15;
                    shotCounterBasic++;
                    updateAngleMod();
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPattern2()
        {
            while (true)
            {
                if (timeLastShot <= 0)
                {
                    if (shotCounterBasic % 4 == 0)
                    { EntityManager.Add(new FinalBossBullet(this.Position, 60 + (float)angleModBasic)); }
                    else if (shotCounterBasic % 4 == 1)
                    { EntityManager.Add(new FinalBossBullet(this.Position, 80 + (float)angleModBasic)); }
                    else if (shotCounterBasic % 4 == 2)
                    { EntityManager.Add(new FinalBossBullet(this.Position, 100 + (float)angleModBasic)); }
                    else if (shotCounterBasic % 4 == 3)
                    { EntityManager.Add(new FinalBossBullet(this.Position, 120 + (float)angleModBasic)); }
                    timeLastShot = .2;
                    shotCounterBasic++;
                    updateAngleMod();
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternFourStream()
        {
            while (true)
            {
                if (timeLastShot <= 0)
                {
                    EntityManager.Add(new FinalBossBullet(this.Position, 50 + (float)angleModBasic));
                    EntityManager.Add(new FinalBossBullet(this.Position, 75 + (float)angleModBasic));
                    EntityManager.Add(new FinalBossBullet(this.Position, 105 + (float)angleModBasic));
                    EntityManager.Add(new FinalBossBullet(this.Position, 130 + (float)angleModBasic));
                    timeLastShot = .3;
                    shotCounterBasic++;
                    updateAngleMod();
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternCurveBurst()
        {
            while (true)
            {

                if (timeLastShotCurveBurst <= 0)
                {
                    EntityManager.Add(new FinalBossBullet(Position, -36 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, -24 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, -12 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, 0 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, 12 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, 24 + (float)angleModCurveBurst));
                    EntityManager.Add(new FinalBossBullet(Position, 36 + (float)angleModCurveBurst));


                    timeLastShotCurveBurst = .35;
                    updateAngleModCurveBurst();
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternFiveBurst()
        {
            while (true)
            {

                if (timeLastShotFiveBurst <= 0)
                {
                    EntityManager.Add(new FinalBossBullet(Position, 180 + (float)angleModFiveBurst));

                    timeLastShotFiveBurst = .15;
                    shotCounterFiveBurst++;
                    if (shotCounterFiveBurst % 5 == 0)
                    {
                        updateAngleModFiveBurst();
                    }
                }
                yield return 0;
            }
        }

        IEnumerable<int> FinalBossBulletPatternShrapnel()
        {
            while (true)
            {
                if (timeLastShotShrapnel <= 0)
                {
                    EntityManager.Add(new FinalBossBulletShrapnel(Position, 0 + (float)angleModShrapnel));
                    updateAngleModShrapnel();
                    timeLastShotShrapnel = .5;
                }
                yield return 0;
            }
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

        protected override void Fire()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
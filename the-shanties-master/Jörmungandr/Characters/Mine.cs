using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Jörmungandr.Bullets.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jörmungandr.Characters
{
    /// <summary>
    /// Mine class. Behaves like a bullet.
    /// Spawns and moves towards the direction of the player.
    /// When it comes within proximity of the player, explodes into bullets.
    /// </summary>
    public class Mine : Enemy
    {
        public Mine(AbstractBulletFactory bulletFactory, Texture2D image, Vector2 position, int points, int fireRate, int health) : base(bulletFactory, image, position, points, fireRate, health)
        {
            this.Position = position;
            this.sprite = Art.Mine;
            this.Velocity = new Vector2(2, 2);
            
            // figure out where the player is and face towards it
            this.Orientation = (float)(Math.PI - Math.Atan2(-this.Position.Y + Player.Instance.Position.Y, -Player.Instance.Position.X + this.Position.X));

        }
        
        public override void Update(GameTime gt)
        {
            base.Update(gt);
            
            // if it is in proximity of the player, explode into more bullets
            if (Vector2.Distance(this.Position, Player.Instance.Position) <= 30)
            {
                this.Expire();
                this.didExplode = true;
            }

            if ((Position.X > 630 || Position.X < -30) || (Position.Y > 830 || Position.Y < -30) || this.IsExpired)
            {
                this.Expire();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: add scale instead of hardcoding the division
            spriteBatch.Draw(this.sprite,
                new Rectangle((int) Position.X, (int) Position.Y, (int)(sprite.Width/1.5), (int)(sprite.Height/1.5)), null,
                Color.White,
                Orientation,
                Vector2.Zero,
                SpriteEffects.None, 0);
        }

        public override void Expire()
        {
            base.Expire();

            // only fire if exploded by player
            if (didExplode)
                this.Fire();
        }

        protected override void Fire()
        {
            for (int a = 0; a <= 360; a += 30)
            {
                EntityManager.Add(this.bulletFactory.CreateBullet(this.Position, a));
            }
        }
    }
}

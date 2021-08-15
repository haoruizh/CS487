using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item
{
    public abstract class PlayerItem:BasicItem
    {
        /// <summary>
        /// constructor of PlayerItem. Apply fall down behavior as default
        /// </summary>
        /// <param name="image">sprite of item</param>
        /// <param name="position">spawn position of this item</param>
        public PlayerItem(Texture2D image, Vector2 position) :base(image, position)
        {
            this.AddBehavior(FallDownBehavior());
        }

        /// <summary>
        /// Apply item affect on curPlayer
        /// </summary>
        /// <param name="curPlayer">current player</param>
        public override void DoAffect(Player curPlayer){}

        /// <summary>
        /// Add item behavior
        /// </summary>
        /// <param name="behavior">current behavior</param>
        protected void AddBehavior(IEnumerable<int> behavior)
        {
            behaviors.Add(behavior.GetEnumerator());
        }

        /// <summary>
        /// Apply a behavior
        /// </summary>
        protected void ApplyBehaviors()
        {
            for (int i = 0; i < behaviors.Count; i++)
            {
                if (!behaviors[i].MoveNext())
                    behaviors.RemoveAt(i--);
            }
        }

        /// <summary>
        /// Update current status based on game time
        /// </summary>
        /// <param name="gt">current GameTime</param>
        public override void Update(GameTime gt)
        {
            if (this.timeUntilStart <= 0)
            {
                ApplyBehaviors();
            }
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }
            Position += Velocity;
            Velocity *= 1.0f;

            base.Update(gt);

            if (Position.Y >= 830 || this.IsExpired)
            {
                this.Expire();
            }
            // if it is in proximity of the player, collision
            if (Vector2.Distance(this.Position, Player.Instance.Position) <= 30)
            {
                this.Expire(); // change to handle collision
            }
        }

        #region Behaviors
        IEnumerable<int> FallDownBehavior()
        {
            Orientation = 0;
            Velocity = new Vector2(0, 0);
            float posX, posY;

            while (true)
            {
                posY = Position.Y;
                posX = Position.X;

                Position.Y += 3;
                if (Position.Y > 816)
                {
                    //Position.Y = -16;
                    //Position.X = rand.Next(0, 600);
                    this.Expire();
                }
                yield return 0;
            }

        }
        #endregion
    }
}

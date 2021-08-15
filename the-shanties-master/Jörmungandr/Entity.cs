using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Jörmungandr.Behavior;

namespace Jörmungandr
{
    /// <summary>
    /// Class for all entities -- bullets, enemies, and the player should inherit from this class.
    /// This class should be a good template for anything that needs a sprite.
    /// Code adapted from monogame tutorial: https://github.com/MonoGame/MonoGame.Samples/blob/develop/NeonShooter/NeonShooter.Core/Game/ 
    /// </summary>
    public abstract class Entity
    {
        protected Texture2D sprite;
        protected Color color = Color.White;
        protected List<IAction> behaviors = new List<IAction>();

        public bool IsExpired;

        public Vector2 Position;
        public float Orientation;
        public Vector2 Velocity;

        public abstract void Update(GameTime gt);

        public Vector2 Size => sprite == null ? Vector2.Zero : new Vector2(sprite.Width, sprite.Height);

        /// <summary>
        /// Draw the entity on the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch that draw the sprite.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }

        /// <summary>
        /// Hide this entity
        /// </summary>
        public virtual void Expire()
        {
            this.IsExpired = true;
            this.sprite = Art.Hide;
        }

        /// <summary>
        /// Add behaviors to this entity
        /// </summary>
        /// <param name="newBehavior">new behavior add to this entity.</param>
        public void AddBehavior(IAction newBehavior)
        {
            this.behaviors.Add(newBehavior);
        }

        /// <summary>
        /// Execute each behavior in this entity
        /// </summary>
        protected void ApplyBehaviors()
        {
            foreach (IAction behavior in behaviors)
            {
                behavior.ExecuteAction(this);
            }
        }
    }
}

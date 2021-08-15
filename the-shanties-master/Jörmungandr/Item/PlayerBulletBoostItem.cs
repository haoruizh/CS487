using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Jörmungandr.Bullets.Factory;

namespace Jörmungandr.Item
{
    public class PlayerBulletBoostItem : PlayerItem
    {
        /// <summary>
        /// Player bullet boost item constructor
        /// </summary>
        /// <param name="image">sprite of this item.</param>
        /// <param name="position">spawn position of this item.</param>
        public PlayerBulletBoostItem(Texture2D image, Vector2 position) :base(image, position)
        { }

        /// <summary>
        /// DO NOTHING
        /// </summary>
        /// <param name="curEnemy"></param>
        public override void DoAffect(Enemy curEnemy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change current player bullet factory to power bullet factory.
        /// </summary>
        /// <param name="curPlayer"> current player. </param>
        public override void DoAffect(Player curPlayer)
        {
            curPlayer.SetFactory(new PlayerPowerBulletFactory());
        }

        public override void UnAffect(Player curPlayer)
        {
            curPlayer.SetFactory(new PlayerBulletFactory());
        }

        public override void UnAffect(Enemy curEnemy)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item
{
    public class HealPlayerItem : PlayerItem
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="image">sprite of item</param>
        /// <param name="position">spawn position of item</param>
        public HealPlayerItem(Texture2D image, Vector2 position):base(image, position)
        {

        }

        /// <summary>
        /// apply item affect on player. For this one, heal player by call player function
        /// </summary>
        /// <param name="curPlayer">current player</param>
        public override void DoAffect(Player curPlayer)
        {
            curPlayer.RecoverLife();
        }

        /// <summary>
        /// Necessary implementation. Should have nothing useful
        /// </summary>
        /// <param name="curEnemy">current enemy</param>
        public override void DoAffect(Enemy curEnemy)
        {
            throw new NotImplementedException();
        }

        public override void UnAffect(Player curPlayer)
        {
            throw new NotImplementedException();
        }

        public override void UnAffect(Enemy curEnemy)
        {
            throw new NotImplementedException();
        }
    }
}

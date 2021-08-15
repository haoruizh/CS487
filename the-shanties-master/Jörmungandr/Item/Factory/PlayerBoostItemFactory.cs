using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item.Factory
{
    public class PlayerBoostItemFactory : AbstracPlayertItemFactory
    {
        public override PlayerItem CreatePlayerItem(Vector2 position)
        {
            return new PlayerBulletBoostItem(Jörmungandr.Art.EmptyHeart, position);
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item.Factory
{
    public class PlayerHealItemFactory : AbstracPlayertItemFactory
    {
        public override PlayerItem CreatePlayerItem(Vector2 position)
        {
            return new HealPlayerItem(Jörmungandr.Art.HealthCapsule, position);
        }
    }
}

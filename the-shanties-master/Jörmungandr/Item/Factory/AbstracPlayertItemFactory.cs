using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Item
{
    public abstract class AbstracPlayertItemFactory
    {
        public abstract PlayerItem CreatePlayerItem(Vector2 position);
    }
}

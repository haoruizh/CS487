using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Collison
{
    public class PlayerItemCollision : CollisonInterface
    {
        private Player curPlayer;
        private List<Item.PlayerItem> itemList;

        public PlayerItemCollision(Player curPlayer, List<Item.PlayerItem> itemList)
        {
            this.curPlayer = curPlayer;
            this.itemList = itemList;
        }

        internal void UpdatePlayer(Player player)
        {
            this.curPlayer = player;
        }

        internal void UpdateItemList(List<Item.PlayerItem> itemList)
        {
            this.itemList = itemList;
        }

        public int CollisionDetect()
        {
            for(int curIndex = 0; curIndex < this.itemList.Count; curIndex++)
            {
                if(this.curPlayer.IsCollideWithItem(itemList[curIndex]))
                {
                    return curIndex;
                }

            }
            return -1;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr
{
    public class CollisionManager
    {
        private CollisonInterface collisonDetectionCommand;

        public void SetCommand(CollisonInterface collison)
        {
            this.collisonDetectionCommand = collison;
        }
        public CollisionManager()
        {
        }

        public int collisionDetection()
        {
            return this.collisonDetectionCommand.CollisionDetect();
        }
    }
}

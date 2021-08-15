using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    public class FallDown : IAction
    {
        public void ExecuteAction(Entity actor)
        {
            actor.Orientation = 0;
            actor.Velocity = new Vector2(0, 0);
            float posX, posY;

            posY = actor.Position.Y;
            posX = actor.Position.X;

            actor.Position.Y += 3;
        }

        public void UnexecuteAction(Entity actor)
        {
            throw new NotImplementedException();
        }
    }
}

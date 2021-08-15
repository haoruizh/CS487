using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    public class SlowVelocity : IAction
    {
        public void ExecuteAction(Entity actor)
        {
            actor.Velocity = new Vector2(5, 5);
        }

        public void UnexecuteAction(Entity actor)
        {
            actor.Velocity = new Vector2(10, 10);
        }
    }
}

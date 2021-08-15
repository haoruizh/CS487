using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    /// <summary>
    /// Moves the entity based on its velocity
    /// </summary>
    public class MoveDown : IAction
    {
        public void ExecuteAction(Entity actor)
        {
            actor.Position.Y += actor.Velocity.Y;
        }

        public void UnexecuteAction(Entity actor)
        {
            throw new NotImplementedException();
        }
    }
}

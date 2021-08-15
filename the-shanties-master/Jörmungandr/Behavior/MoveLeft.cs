using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    /// <summary>
    /// Moves the entity based on its velocity
    /// </summary>
    public class MoveLeft : IAction
    {
        public void ExecuteAction(Entity actor)
        {
            actor.Position.X += -actor.Velocity.X;
        }

        public void UnexecuteAction(Entity actor)
        {
            throw new NotImplementedException();
        }
    }
}

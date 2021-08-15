using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    class BuzzinBox : IAction
    {
        int xFlip = 0;
        int yFlip = 0;

        public void ExecuteAction(Entity actor)
        {
            actor.Orientation = 0;
            actor.Velocity = new Vector2(0, 0);
            float posX, posY;

            posY = actor.Position.Y;
            posX = actor.Position.X;
            if (xFlip == 0)
            {
                actor.Position.X += (float)(.5);
                if (actor.Position.X >= 450)
                { xFlip = 1; }
            }
            else
            {
                actor.Position.X -= (float)(.5);
                if (actor.Position.X <= 150)
                { xFlip = 0; }
            }
            if (yFlip == 0)
            {
                actor.Position.Y += (float)(.3);
                if (actor.Position.Y >= 225)
                { yFlip = 1; }
            }
            else
            {
                actor.Position.Y -= (float)(.3);
                if (actor.Position.Y <= 200)
                { yFlip = 0; }
            }
        }

        public void UnexecuteAction(Entity actor)
        {
            throw new NotImplementedException();
        }
    }
}

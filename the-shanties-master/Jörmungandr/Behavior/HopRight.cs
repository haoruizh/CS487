using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Jörmungandr.Behavior
{
    public class HopRight : IAction
    {
		// Private members
		public static Random rand = new Random();
		private Vector2 ScreenSize = new Vector2(600, 800);
        int jumpState = 0, jump = 0;
        bool initialState = true;
        private Vector2 initialPosition;

        public void ExecuteAction(Entity actor)
        {
            //actor.Velocity = new Vector2(0, 0);
            jumpState++;
            if(initialState)
            {
                initialPosition = actor.Position;
                initialState = false;
            }    

            // Move Horizontally
            actor.Position.X += 1;

            // Get horizontal position
            int posX = (int)actor.Position.X;

            switch (posX % 60)
            {
                case 15: // Stagnant
                    actor.Position.Y = this.initialPosition.Y;
                    break;
                case 30: // Jump Up
                    actor.Position.Y += 30;
                    break;
                case 45: // Jump Down
                    actor.Position.Y -= 15;
                    break;
                case 60: // JumpDown
                    actor.Position.Y -= 15;
                    break;
            }
        }

        public void UnexecuteAction(Entity actor)
        {
            throw new NotImplementedException();
        }
    }
}

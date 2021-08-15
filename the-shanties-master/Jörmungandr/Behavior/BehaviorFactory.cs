using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    public class BehaviorFactory
    {
        public IAction CreateBehavior(string behavior)
        {
            switch (behavior)
            {
                case "FallDown":
                    return new FallDown();
                case "HopRight":
                    return new HopRight();
                case "BobInBox":
                    return new BobInBox();
                case "BuzzinBox":
                    return new BuzzinBox();
            }
            return null;
        }
    }
}

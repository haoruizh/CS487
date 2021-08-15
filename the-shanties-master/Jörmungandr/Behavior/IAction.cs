using System;
using System.Collections.Generic;
using System.Text;

namespace Jörmungandr.Behavior
{
    public interface IAction
    {
        public void ExecuteAction(Entity actor);
        public void UnexecuteAction(Entity actor);
    }
}

using FoodStandUI.View.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.Resources.Messages
{
    class FoodMessage
    {
        public FoodMessage(FoodListView view, Action action)
        {
            Type = action;
            View = view;
        }

        public FoodListView View { get; }

        public Action Type { get; }

        internal enum Action
        {
            SaveSettings,
            CancelSettings,
        }
    }
}

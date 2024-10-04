using FoodStandUI.View.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.Resources.Messages
{
    internal class ContainerMessage
    {
        public ContainerMessage(ContianerSettingsView view, Action action)
        {
            Type = action;
            View = view;
        }

        public ContianerSettingsView View { get; }

        public Action Type { get; }

        internal enum Action
        {
            SaveSettings,
            CancelSettings,
        }
    }
}

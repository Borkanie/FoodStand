using FoodStandUI.View.Dialog;
using FoodStandUI.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.Resources.Messages
{

    internal class ContainerViewModelMessage
    {
        public enum Action
        {
            OpenSettings,
        }

        public ContainerViewModelMessage(ContainerViewModel sender, Action type)
        {
            ViewModel = sender;
            Type = type;
        }

        public ContainerViewModel ViewModel { get; }

        public Action Type { get; }
    }
}

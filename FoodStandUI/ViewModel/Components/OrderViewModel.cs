using FoodMeasuringObjects.Orders;
using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class OrderViewModel : BaseViewModel
    {
        Order model;
        public OrderViewModel(Order model)
        {
            this.model = model;
            Items = [.. model.Items];
            Items.CollectionChanged += InternalItemCollectionChanged;
        }

        private void InternalItemCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    return;
            }
        }

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    
        public int GetTotalCost()
        {
            return model.GetTotalCost();
        }
    }
}

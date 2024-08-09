using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class ItemViewModel : BaseViewModel
    {
        Item model;

        public ItemViewModel()
        {
            
        }

        public ItemViewModel(Item model)
        {
            this.model = model;
        }

        public int Cost
        {
            get
            {
                return model.Cost;
            }
        }

        public Food Food
        {
            get
            {
                return model.Food;
            }
            set
            {
                if (model.Food != value)
                {
                    model.Food = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get
            {
                return model.Quantity;
            }
            set
            {
                if (model.Quantity != value)
                {
                    model.Quantity = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public FoodPortioningType Type
        {
            get
            {
                return model.Type;
            }
            set
            {
                if (model.Type != value)
                {
                    model.Type = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}

using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodStandUI.ViewModel.Basic;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FoodStandUI.ViewModel.Components
{
    internal class ContainerViewModel : BaseViewModel
    {
        FoodContainer model;
        private double _fontSizeTitle;
        private double _fontSizeDescription;
        private readonly double minTitleSize = 10;
        private readonly double minDescriptionSize = 10;
        public ContainerViewModel(FoodContainer model)
        {
            Model = model;
        }
        public FoodContainer Model
        {
            get => model;
            set
            {
                if(model is null || model != value)
                {
                    model = value;
                    RaisePropertyChanged(nameof(Model));
                }
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
                    BackendAPI.Instance.Container.Resolve<ILocalizationService>().AddFood(value, model.Location);
                    this.RaisePropertyChanged();
                }
            }
        }

        public double FontSizeTitle
        {
            get => _fontSizeTitle;
            set
            {
                if (_fontSizeTitle != value)
                {
                    _fontSizeTitle = value;
                    RaisePropertyChanged();
                }
            }
        }

        public double FontSizeDescription
        {
            get => _fontSizeDescription;
            set
            {
                if (_fontSizeDescription != value)
                {
                    _fontSizeDescription = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override double Heigth
        { 
            get
            {
                return heigth;
            } 
            set
            {
                if(heigth != value) 
                {
                    heigth = value;
                    RecalculateFontSize();
                    RaisePropertyChanged();
                } 
            }
        }

        public override double Width 
        {
            get
            {
                return width;
            }

            set 
            {
                if(width != value)
                {
                    width = value;
                    RecalculateFontSize();
                    RaisePropertyChanged();
                }
            }

        }

        private void RecalculateFontSize()
        {
            FontSizeTitle = Math.Max(Math.Min(heigth, width) / 6, minTitleSize);
            FontSizeDescription = Math.Max(Math.Min(heigth, width) / 10, minDescriptionSize);
        }

        public String Quantity
        {
            get
            {
                if (Model.Type == FoodPortioningType.Piece)
                    return model.AvailableQuantity + " pieces";
                else
                    return model.AvailableQuantity + " g";
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

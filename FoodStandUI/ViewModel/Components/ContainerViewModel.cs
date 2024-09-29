using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodStandUI.Resources;
using FoodStandUI.ViewModel.Basic;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Messaging;
using FoodStandUI.Resources.Messages;
using System.ComponentModel;

namespace FoodStandUI.ViewModel.Components
{
    internal class ContainerViewModel : BaseViewModel
    {
        FoodContainer model;
        private double _fontSizeTitle;
        private double _fontSizeDescription;
        private readonly double minTitleSize = 10;
        private readonly double minDescriptionSize = 10;
        private FoodViewModel _food;
        private bool isFoodSettingsVisible;

        public ContainerViewModel(FoodContainer model)
        {
            Model = model;
            _food = new FoodViewModel(model.Food);
            OnSettingsCLicked = new Command(() => WeakReferenceMessenger.Default.Send(new ContainerViewModelMessage(this, ContainerViewModelMessage.Action.OpenSettings)));
            Button1Command = new Command(ToggleVisibility);
            Button2Command = new Command(ToggleVisibility);

        }

        #region switching

        public ICommand Button1Command { get; }
        public ICommand Button2Command { get; }

        public double Button1Opacity => isFoodSettingsVisible ? 1 : 0;
        public double Button2Opacity => isFoodSettingsVisible ? 0 : 1;

        private void ToggleVisibility()
        {
            isFoodSettingsVisible = !isFoodSettingsVisible;
            RaisePropertyChanged(nameof(Button1Opacity));
            RaisePropertyChanged(nameof(Button2Opacity));
        }

        #endregion

        public bool UpdateModel()
        {
            return true;
        }

        public ICommand OnSettingsCLicked { get; private set; }

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

        public FoodViewModel Food
        {
            get
            {
                return _food;
            }
            set
            {
                if (_food != value)
                {
                    _food = value;
                    _food.AddToLocation(model.Location);
                    RaisePropertyChanged();
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

        public int Line
        {
            get => model.Location.Line;
            set
            {
                if(model.Location.Line != value)
                {
                    model.Location.Line = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Column
        {
            get => model.Location.Column;
            set
            {
                if (model.Location.Column != value)
                {
                    model.Location.Column = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}

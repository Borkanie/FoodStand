using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Basic
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        protected double width = 150;
        protected double heigth = 150;

        public virtual double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value != width)
                {
                    width = value;
                    RaisePropertyChanged();
                }
            }
        }

        public virtual double Heigth
        {
            get
            {
                return heigth;
            }
            set
            {
                if (value != heigth)
                {
                    heigth = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

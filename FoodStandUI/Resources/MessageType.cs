using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.Resources
{
    public class MessageType
    {
        private MessageType(string value) { Value = value; }

        public string Value { get; private set; }

        public static MessageType ContainerSettingsButtonClicked { get { return new MessageType("ContainerSettingsButtonClicked"); } }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

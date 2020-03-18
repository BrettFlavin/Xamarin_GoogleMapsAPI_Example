using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace HW5_MapApp
{
    public class Location
    {
        public Position Position { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HW5_MapApp
{
   
    public partial class SatelliteMapPage : ContentPage
    {
        // a collection of Location objects that holds all the pinned Locations
        public ObservableCollection<Location> PinnedLocationList { get; private set; } = new ObservableCollection<Location>
            {
                new Location {Position = new Position(33.197563, -117.245807), Address = "Frazier Farms", Description = "Organic and Farm fresh food market"},
                new Location {Position = new Position(33.197381, -117.245898), Address = "Crunch Gym", Description = "Lift heavy weights and stuff.."},
                new Location {Position = new Position(32.830351, -117.280056), Address = "GFs House", Description = "Windansea Beach, La Jolla"},
                new Location {Position = new Position(33.492097, -112.104575), Address = "Born Here", Description = "First House in Phoenix, AZ"},
                new Location {Position = new Position(39.954076, -75.163098), Address = "Center City Philadelphia", Description = "Come get a cheesesteak"}
            };

        public SatelliteMapPage()
        {
            InitializeComponent();
            InitializeMap();          
            BindingContext = this;
        }

        // sets the starting position of the map and the ItemSource of the Picker
        public void InitializeMap()
        {
            // set the maps starting position
            Position startPosition = new Position(38.995264, -94.279136);
            var StartMapLocation = MapSpan.FromCenterAndRadius(startPosition, Distance.FromMiles(1500));
            MyMap.MoveToRegion(StartMapLocation);
        
            // set the picker ItemSource 
            List<string> pickerList = new List<string>();
            foreach (Location L in PinnedLocationList)
                pickerList.Add(L.Address);
            picker.ItemsSource = pickerList;
        }
        
        // update map to center on picker location 
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var PinLocation = MapSpan.FromCenterAndRadius(MyMap.Pins[selectedIndex].Position, Distance.FromMiles(5));
                MyMap.MoveToRegion(PinLocation);
            }
        }

        // go to the pin's location when pin is clicked on
        private void Pin_Clicked(object sender, EventArgs e)
        {
            var thePosition = ((Pin)sender).Position;
            var PinLocation = MapSpan.FromCenterAndRadius(thePosition, Distance.FromMiles(1));
            MyMap.MoveToRegion(PinLocation);
        }

        // change the Map's MapType when button is clicked
        private void ChangeMapButton_Clicked(object sender, EventArgs e)
        {
            MapType typeofMap = MyMap.MapType;
            switch (typeofMap.ToString())
            {
                case "Satellite":
                    MyMap.MapType = MapType.Street;
                    break;
                case "Street":
                    MyMap.MapType = MapType.Hybrid;
                    break;
                case "Hybrid":
                    MyMap.MapType = MapType.Satellite;
                    break;
            }   
        }

        // reset the Map to default position
        private void ResetMapButton_Clicked(object sender, EventArgs e)
        {
            InitializeMap();
            picker.SelectedIndex = -1;
           
            // Make sure all pins get deselected here
        }

       
    }
}

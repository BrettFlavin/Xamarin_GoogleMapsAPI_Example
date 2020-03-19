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
        // the default start position of the map
        public Position StartPos = new Position(38.995264, -94.279136);

        public SatelliteMapPage()
        {
            InitializeComponent();
            InitializeMap();

            // set the picker ItemSource to display a list of all Location addresses
            List<string> pickerList = new List<string>();
            foreach (var location in PinnedLocationList)
                pickerList.Add(location.Address);
            picker.ItemsSource = pickerList;

            BindingContext = this;
        }

        // sets the starting position of the map and the ItemSource of the Picker
        public void InitializeMap()
        {
            // set the Map to the start position
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(StartPos, Distance.FromMiles(1500)));

            // set the label to show current location
            LocationLabel.Text = $"Latitude: {StartPos.Latitude}\nLongitude: {StartPos.Longitude}";

            // set the label to show current MapType
            MapLabel.Text = $"{MyMap.MapType.ToString()}";
        }
        
        // event fired when the picker SelectedItemIndex changes 
        // centers the map on the pin location and updates the location label 
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the index of the picker item
            int selectedIndex = ((Picker)sender).SelectedIndex;

            // update map if index has changed
            if (selectedIndex != -1)
            {
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MyMap.Pins[selectedIndex].Position, Distance.FromMiles(1)));
                
                // set the label to show current location
                LocationLabel.Text = $"Latitude: {MyMap.Pins[selectedIndex].Position.Latitude}\nLongitude: {MyMap.Pins[selectedIndex].Position.Longitude}";
            }
        }

        // event fired when a Map Pin is clicked on
        // moves the Map to the Pin location and updates the location label
        private void Pin_Clicked(object sender, EventArgs e)
        {
            // move map to the pin's location
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(((Pin)sender).Position, Distance.FromMiles(1)));
            // set the label to show current location
            LocationLabel.Text = $"Latitude: {((Pin)sender).Position.Latitude}\nLongitude: {((Pin)sender).Position.Longitude}";
        }
        
        // event fired when the 'Change Map' button is clicked 
        // changes the Map's MapType property
        private void ChangeMapButton_Clicked(object sender, EventArgs e)
        {
            // get the current MapType property of Map (MapType = Satellite, Street, Hybrid)
            MapType typeofMap = MyMap.MapType;

            // change the current MapType property
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
            // set the label to show current MapType
            MapLabel.Text = $"{MyMap.MapType.ToString()}";           
        }

        // reset the Map to default position
        private void ResetMapButton_Clicked(object sender, EventArgs e)
        {
            InitializeMap();
            picker.SelectedIndex = -1;
           
            // *** TO DO ***
            // Make sure all pins get deselected here
        }

        // event fired when user clicks somewhere on Map 
        // updates the label with the position clicked on
        private void MyMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            // set the label to show current location
            LocationLabel.Text = $"Latitude: {e.Position.Latitude}\nLongitude: {e.Position.Longitude}";
        }
    }
}

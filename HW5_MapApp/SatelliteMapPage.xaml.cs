using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HW5_MapApp
{

    public partial class SatelliteMapPage : ContentPage
    {
        // a collection of Location objects that holds all the pinned Locations
        public ObservableCollection<Location> PinnedLocationList { get; private set; } = new ObservableCollection<Location>
         {
            new Location {Position = new Position(33.197563, -117.245807), Address = "Vista, CA", Description = "Frazier Farms - Organic food market"},
            new Location {Position = new Position(32.830351, -117.280056), Address = "San Diego, CA", Description = "GFs house - Windansea Beach, La Jolla"},
            new Location {Position = new Position(33.492097, -112.104575), Address = "Phoenix, AZ", Description = "First house - Born here in Phoenix, AZ"},
            new Location {Position = new Position(39.950084, -75.144705), Address = "Philadelphia, PA", Description = "Sonny's steaks - Best cheesesteak in Philly"},
            new Location {Position = new Position(26.459279, 127.918681), Address = "Okinawa, Japan", Description = " Island of Okinawa - Lived here for 1 year"},
        };
        // a default start position for the map
        public Position StartPos = new Position(38.995264, -97.279136);

        public SatelliteMapPage()
        {
            InitializeComponent();
            InitializeMap();
            SetPicker();
        }

        // sets Map starting position and labels
        public void InitializeMap()
        {
            // set Map to default start position, set labels to show location and map type
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(StartPos, Distance.FromMiles(1500)));
            LocationLabel.Text = $"Latitude: {StartPos.Latitude}\nLongitude: {StartPos.Longitude}";
            MapLabel.Text = $"{MyMap.MapType.ToString()}";
        }

        // Picker ItemSource will display the address of every Location in PinnedLocationList
        public void SetPicker()
        {
            List<string> pickerList = new List<string>();
            foreach (var location in PinnedLocationList)
                pickerList.Add(location.Address);
            picker.ItemsSource = pickerList;
            BindingContext = this;
        }

        // event fired when a Pin is selected from the picker 
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the index of the picker item selected and update map if index has changed
            int selectedIndex = ((Picker)sender).SelectedIndex;
            if (selectedIndex != -1)
            {
                // center the map on the selected pin's location and update the location label 
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MyMap.Pins[selectedIndex].Position, Distance.FromMiles(5)));
                LocationLabel.Text = $"Latitude: {MyMap.Pins[selectedIndex].Position.Latitude}\nLongitude: {MyMap.Pins[selectedIndex].Position.Longitude}";
            }
        }

        // event fired when a Map Pin is clicked on
        private void Pin_Clicked(object sender, EventArgs e)
        {
            // moves the Map to the Pin location and updates the location label
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(((Pin)sender).Position, Distance.FromMiles(.5)));
            LocationLabel.Text = $"Latitude: {((Pin)sender).Position.Latitude}\nLongitude: {((Pin)sender).Position.Longitude}";
        }
        
        // event fired when the 'Change Map' button is clicked 
        private void ChangeMapButton_Clicked(object sender, EventArgs e)
        {
            // get the current MapType property of Map (enum MapType = Satellite, Street, Hybrid)
            MapType typeofMap = MyMap.MapType;

            // change the current MapType 
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

        // event fired when the 'Reset Map' button is clicked
        private void ResetMapButton_Clicked(object sender, EventArgs e)
        {
            // reset the picker and map to default positions
            picker.SelectedIndex = -1;
            InitializeMap();
        }

        // event fired when user clicks somewhere on Map 
        private void MyMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            // updates the label to show coordinates of position clicked on
            LocationLabel.Text = $"Latitude: {e.Position.Latitude}\nLongitude: {e.Position.Longitude}";
        }

    }
}

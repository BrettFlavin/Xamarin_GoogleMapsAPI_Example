using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HW5_MapApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new SatelliteMapPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

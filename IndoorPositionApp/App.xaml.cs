using IndoorPositionApp.Model;
using System;
using Xamarin.Forms;

namespace IndoorPositionApp
{

    public partial class App : Application
    {
        public App(String filename)
        {
            InitializeComponent();

            //Conexion para registro de Usuario
            Connection.Initialize(filename);
            MainPage = new MainPage();
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

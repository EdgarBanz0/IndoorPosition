using System;
using Xamarin.Forms;

namespace IndoorPositionApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnEnter_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new IndoorPositionApp.Pages.Interface(0);
        }
    }
}

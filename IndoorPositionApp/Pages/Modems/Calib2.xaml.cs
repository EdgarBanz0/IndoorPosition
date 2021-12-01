using IndoorPositionApp.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages.Modems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calib2 : ContentPage
    {
        int bandera = 0;
        List<decimal> ks = new List<decimal>();
        List<string> ssids = new List<string>();
        public Calib2()
        {

            InitializeComponent();
            InitPicker();

        }


        private async void InitPicker()
        {

            try
            {

                string rssiP = Connection.Instance.ReturnRSSI(2);

                labelRSSI1.Text = rssiP;

                string valorK = Connection.Instance.ReturnK(2);
                labelK1.Text = valorK;

                string modem = Connection.Instance.ReturnMod(2);
                selectRed.Title = modem;

                // User search = Connection.Instance.SearchK(1);


                //selectRed.Title = search.CalibratedRouters;
                //labelK1.Text = search.ConsK;
                //labelRSSI1.Text = search.pruebRSSI;
            }
            catch
            {
                await DisplayAlert("Error", "No se ha calibrado", "OK");

            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            if (bandera == 0)
            {
                selectRed.IsEnabled = true;
                ubMod1.IsEnabled = true;
                nameMod1.IsEnabled = true;
                btnMod.Text = "Guardar cambios";
                btnMod.BackgroundColor = Color.Red;
                bandera = 1;
            }
            else
            {
                selectRed.IsEnabled = false;
                ubMod1.IsEnabled = false;
                nameMod1.IsEnabled = false;
                btnMod.Text = "Modificar";
                btnMod.BackgroundColor = Color.FromHex("#66ccff");
                UMmod1.Text = DateTime.Now.ToString("G");
                bandera = 0;
            }


        }

        private void selectRed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btnCal_Clicked(object sender, EventArgs e)
        {
            string mod = selectRed.Title;
            Connection.Instance.bandCal(1);
            await Navigation.PushAsync(new Calibration());

        }
    }
}
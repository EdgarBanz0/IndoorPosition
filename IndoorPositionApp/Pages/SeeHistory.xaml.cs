using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeHistory : ContentPage
    {
        int idLook;
        public SeeHistory(int idSearch)
        {
            idLook = idSearch;
            InitializeComponent();
            var users = Connection.Instance.GetHistory(idSearch);
            recordList.ItemsSource = users;
        }

        private async void btnClean_Clicked(object sender, EventArgs e)
        {
            int error;
            error = Connection.Instance.DeleteRecord(idLook);

            if (error == 0)
                await DisplayAlert("Error", "No se logro conectar con la base de datos", "OK");
            else
            {
                await DisplayAlert("Listo", "El historial se elimino", "OK");
                await Navigation.PopAsync();
            }


        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            int error;
            error = Connection.Instance.DeleteFollowing(idLook);

            if (error == 0)
                await DisplayAlert("Error", "No se logro conectar con la base de datos", "OK");
            else
            {
                await DisplayAlert("Listo", "Ya no recibira la actividad de este usuario", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
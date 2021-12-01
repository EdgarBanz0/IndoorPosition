using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tracing : ContentPage
    {
        public Tracing()
        {
            InitializeComponent();
        }

        private async void btnFollow_Clicked(object sender, EventArgs e)
        {
            int validate;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                await DisplayAlert("Error", "Realize primero una busqueda de usuario", "OK");
                txtSearch.Focus();
                return;
            }
            try
            {
                validate = Connection.Instance.FollowUser(int.Parse(txtSearch.Text));

                if (validate == 0)
                    await DisplayAlert("Error", "Ya estas monitoreando a este usuario", "OK");
                if (validate == 1)
                    await DisplayAlert("Error", "Ya estas monitoreando al maximo de usuarios", "OK");
                if (validate == 3)
                    await DisplayAlert("Listo", "Has empezado a monitorear a " + txtName.Text, "OK");

                txtSearch.Text = null;
                txtName.Text = null;
                txtAge.Text = null;
                txtEmail.Text = null;

            }
            catch
            {
                await DisplayAlert("Error", "No se pudo actualizar los datos", "OK");
            }
        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {
            //Validacion de busqueda
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                await DisplayAlert("Error", "No se ingreso un Id", "OK");
                txtSearch.Focus();
                return;
            }

            try
            {
                User search = Connection.Instance.SearchUser(int.Parse(txtSearch.Text));
                txtName.Text = search.Name;
                txtAge.Text = search.Age;
                txtEmail.Text = search.Email;
            }
            catch
            {
                await DisplayAlert("Error", "No se encontro el ID", "OK");

            }
        }
    }
}
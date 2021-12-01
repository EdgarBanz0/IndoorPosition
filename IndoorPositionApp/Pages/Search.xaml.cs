using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {
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

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Error", "Realize primero una busqueda de usuario", "OK");
                txtSearch.Focus();
                return;
            }
            else
            {
                Connection.Instance.DeleteUser(int.Parse(txtSearch.Text));
                await DisplayAlert("Listo", "El usuario " + txtName.Text + " se elimino el usuario", "OK");
                txtSearch.Text = null;
                txtName.Text = null;
                txtAge.Text = null;
                txtEmail.Text = null;
            }

        }
    }
}
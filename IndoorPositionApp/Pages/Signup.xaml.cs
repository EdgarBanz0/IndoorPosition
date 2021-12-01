using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {

        int bandera = 0;
        public Signup()
        {
            InitializeComponent();
        }

        private async void btnNew_Clicked(object sender, EventArgs e)
        {

            //Validacion de Registro 
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Error", "No se ingreso un nombre", "OK");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                await DisplayAlert("Error", "No se ingreso una edad", "OK");
                txtAge.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Error", "No se ingreso una direccion de correo valida", "OK");
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "No se ingreso una contrasena", "OK");
                txtPassword.Focus();
                return;
            }

            try
            {
                string ide = Id.ToString();
                Connection.Instance.AddUser(txtName.Text, txtAge.Text, txtEmail.Text, txtPassword.Text);
                User search = Connection.Instance.LoginUser(txtEmail.Text, txtPassword.Text);

                int numero = Connection.Instance.ReturnID(txtEmail.Text);


                bool res = await DisplayAlert("Se ha registrado", txtName.Text + "\nID: " + numero, "Ingresar", "Salir");



                if (res == true)
                {

                    Application.Current.MainPage = new Interface(1);
                }
                txtName.Text = null;
                txtAge.Text = null;
                txtEmail.Text = null;
                txtPassword.Text = null;
                await Navigation.PopAsync();

            }
            catch
            {
                await DisplayAlert("Error", "Fallo en la conexion, intentelo mas tarde", "OK");
            }


        }

        private void seePas_Clicked(object sender, EventArgs e)
        {

            if (bandera == 0)
            {

                txtPassword.IsPassword = false;
                seePas.BackgroundColor = Color.LightBlue;
                bandera = 1;
            }
            else
            {
                txtPassword.IsPassword = true;
                seePas.BackgroundColor = Color.Transparent;
                bandera = 0;
            }



        }
    }
}
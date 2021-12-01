using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        int bandera = 0;
        int permanente = 0;
        int entra = 0;
        string guardado1 = "";
        string guardado2 = "";
        int bandeGuar = 0;
        public Login()
        {
            InitializeComponent();

            inicio();
        }

        private void inicio()
        {

            txtEmail.Text = Connection.Instance.returnCor();
            txtPassword.Text = Connection.Instance.returnContra();
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            //Validacion de inicio de sesion 
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Error", "No se ingreso una direccion de correo valida", "OK");
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "No se ingreso una contraseña", "OK");
                txtPassword.Focus();
                return;
            }

            //Evitar bloqueo de aplicacion
            btnLogin.IsEnabled = false;
            try
            {
                if (swtMan.IsToggled == true)
                {

                    Connection.Instance.guardados(txtEmail.Text, txtPassword.Text); //ENVIO PARA GUARDARLOS
                    Connection.Instance.Permanente(1);//envio la bandera de alto
                }
                else if (swtMan.IsToggled == false)
                {

                    Connection.Instance.guardados("", ""); //ENVIO PARA GUARDARLOS
                    Connection.Instance.Permanente(0);//envio la bandera de alto
                }

                User search = Connection.Instance.LoginUser(txtEmail.Text, txtPassword.Text);
                await DisplayAlert("Accediste Como", search.Name, "OK");
                Application.Current.MainPage = new Interface(1);


            }
            catch
            {
                await DisplayAlert("Error", "Usuario o contrasena Incorrectos", "OK");
                txtEmail.Focus();
            }
            btnLogin.IsEnabled = true;
        }



        private async void btnReg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }

        private void seePas2_Clicked(object sender, EventArgs e)
        {
            if (bandera == 0)
            {

                txtPassword.IsPassword = false;
                seePas2.BackgroundColor = Color.LightBlue;
                bandera = 1;
            }
            else
            {
                txtPassword.IsPassword = true;
                seePas2.BackgroundColor = Color.Transparent;
                bandera = 0;
            }
        }






    }
}
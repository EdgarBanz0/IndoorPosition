using IndoorPositionApp.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {

        public Perfil()
        {
            InitializeComponent();
            inicia();

        }
        int bandera = 0;
        private void inicia()
        {
            User usuario = Connection.Instance.PerUsuario();

            txtName.Text = usuario.Name;
            txtAge.Text = usuario.Age;
            txtEmail.Text = usuario.Email;
            txtPassword.Text = usuario.Password;
        }

        private void btnMOD_Clicked(object sender, EventArgs e)
        {



            if (bandera == 0)
            {

                txtName.IsEnabled = true;
                txtAge.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnMOD.Text = "Guardar cambios";
                bandera = 1;
            }
            else
            {
                string nom = txtName.Text;
                String age = txtAge.Text;
                string email = txtEmail.Text;
                string pass = txtPassword.Text;
                Connection.Instance.ModPer(nom, age, email, pass);
                txtName.IsEnabled = false;
                txtAge.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPassword.IsEnabled = false;
                btnMOD.Text = "Modificar";
            }




        }
    }
}
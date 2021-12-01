using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Interface : MasterDetailPage
    {
        internal List<LateralOptions> LateralOptions { get; set; }
        public Interface(int permission)
        {
            InitializeComponent();
            LateralOptions = new List<LateralOptions>();
            if (permission == 0)
            {
                this.Detail = new NavigationPage(new Login());

                LateralOptions pag1 = new LateralOptions() { Title = "Iniciar sesión", Page = typeof(Login) };
                LateralOptions.Add(pag1);
                LateralOptions pag2 = new LateralOptions() { Title = "Registrar usuario", Page = typeof(Signup) };
                LateralOptions.Add(pag2);
                LateralOptions pag3 = new LateralOptions() { Title = "Búsqueda de usuario", Page = typeof(Search) };
                LateralOptions.Add(pag3);
                LateralOptions pag4 = new LateralOptions() { Title = "Usuarios registrados", Page = typeof(All) };
                LateralOptions.Add(pag4);
                btnExit.IsVisible = false;
                btnExit.IsEnabled = false;

            }
            else if (permission == 1)
            {
                this.Detail = new NavigationPage(new Locate2());

                LateralOptions pag1 = new LateralOptions() { Title = "Localización", Page = typeof(Locate2) };
                LateralOptions.Add(pag1);
                LateralOptions pag2 = new LateralOptions() { Title = "Seguimiento", Page = typeof(Tracing) };
                LateralOptions.Add(pag2);
                LateralOptions pag3 = new LateralOptions() { Title = "Usuarios monitoriados", Page = typeof(AllFollow) };
                LateralOptions.Add(pag3);
                LateralOptions pag4 = new LateralOptions() { Title = "Pruebas", Page = typeof(DataCharts) };
                LateralOptions.Add(pag4);
                LateralOptions pag5 = new LateralOptions() { Title = "Calibración general", Page = typeof(Calibration) };
                LateralOptions.Add(pag5);
                LateralOptions pag6 = new LateralOptions() { Title = "Datos de calibración", Page = typeof(DataModem) };
                LateralOptions.Add(pag6);
                LateralOptions pag7 = new LateralOptions() { Title = "Perfil", Page = typeof(Perfil) };
                LateralOptions.Add(pag7);
                btnExit.IsVisible = true;
                btnExit.IsEnabled = true;
            }

            this.ListMenu.ItemsSource = LateralOptions;
            this.ListMenu.ItemSelected += ListMenu_ItemSelected;
        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LateralOptions page = e.SelectedItem as LateralOptions;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page.Page));
            IsPresented = false;

        }


        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new IndoorPositionApp.Pages.Interface(0);

        }
    }
}
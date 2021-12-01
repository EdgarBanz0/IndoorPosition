using IndoorPositionApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataModem : TabbedPage
    {
        int numero = 0;
        public DataModem()
        {
            InitializeComponent();
            Inicia();
        }

        private void Inicia()
        {
            numero = Connection.Instance.returnNM();
            Children.Add(new Pages.Modems.Calib1());
            Children.Add(new Pages.Modems.Calib2());
            Children.Add(new Pages.Modems.Calib3());
            Children.Add(new Pages.Modems.Calib4());
            if (numero > 4)
            {
                Children.Add(new Pages.Modems.Calib5());
            }
            if (numero > 5)
            {
                Children.Add(new Pages.Modems.Calib6());
            }
            if (numero > 6)
            {
                Children.Add(new Pages.Modems.Calib7());
            }
            if (numero > 7)
            {
                Children.Add(new Pages.Modems.Calib8());
            }
            if (numero > 9)
            {
                Children.Add(new Pages.Modems.Calib9());
            }
            if (numero >= 10)
            {
                Children.Add(new Pages.Modems.Calib10());
            }



        }
    }
}
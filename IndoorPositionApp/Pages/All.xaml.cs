using IndoorPositionApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class All : ContentPage
    {
        public All()
        {
            InitializeComponent();
            var usuarios = Connection.Instance.GetAllUsers();
            UserList.ItemsSource = usuarios;
        }
    }
}
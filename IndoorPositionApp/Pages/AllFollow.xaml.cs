using IndoorPositionApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFollow : ContentPage
    {
        User select = new User();
        public AllFollow()
        {
            InitializeComponent();
            var user = Connection.Instance.AllFollowedUsers();
            UserList.ItemsSource = user;
            UserList.ItemSelected += UserList_ItemSelected;
        }

        private async void UserList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            select = (User)e.SelectedItem;
            await Navigation.PushAsync(new SeeHistory(select.Id));

        }
    }
}
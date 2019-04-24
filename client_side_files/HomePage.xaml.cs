using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SPOT_App
{
    public partial class HomePage : ContentPage
    {
        public RestService restService;
        public User user;
        public HomePage(RestService restService, User user)
        {
            this.restService = restService;
            this.user = user;
            InitializeComponent();
            if (user.IsAdmin == "1")
            {
                addUser.IsVisible = true;
            }
        }

        // The following "_Button_Clicked" methods catch events from the buttons created in the "HomePage.xaml" file.
        private async void RequestsPage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestsPage(restService));
        }

        private async void AddUser_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddUserPage(restService));
        }

        // This function causes the application to go back to the root page by popping all non-root pages off the stack of pages.
        // The root page is defined by the following line:
        // MainPage = new NavigationPage(new LoginPage());
        // which is located in the App.xaml.cs file.
        private async void Logout_Button_Clicked(object sender, EventArgs e)
        {
            restService.Logout();
            await Navigation.PopToRootAsync();
        }
    }
}

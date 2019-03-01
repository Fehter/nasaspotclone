using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace test_xamarin_app
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();         
        }

        // The following "_Button_Clicked" methods catch events from the buttons created in the "HomePage.xaml" file.
        private async void RequestsPage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestsPage());
        }

        private async void ListViewPage1_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViewPage1());
        }

        private async void HelloXamlPage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelloXamlPage());
        }            

        private async void GridDemoPage_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GridDemoPage());
        }

        // This function causes the application to go back to the root page by popping all non-root pages off the stack of pages.
        // The root page is defined by the following line:
        // MainPage = new NavigationPage(new LoginPage());
        // which is located in the App.xaml.cs file.
        private async void Logout_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}

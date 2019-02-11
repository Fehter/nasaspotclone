using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace test_xamarin_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();         
        }

        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new fake_login_page());
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
    }
}

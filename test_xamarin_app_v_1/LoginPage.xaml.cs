using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test_xamarin_app
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

        // Then the Login button (which is defined in the LoginPage.xaml file) is clicked, push the HomePage onto the stack of pages.
        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }        
    }
}
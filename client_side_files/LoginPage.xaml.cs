using SPOT_App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOT_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        RestService restService;
        LoginResponse loginResponse;
        User user;
		public LoginPage(RestService restService)
		{
            this.restService = restService;
			InitializeComponent();
		}

        // Then the Login button (which is defined in the LoginPage.xaml file) is clicked, push the HomePage onto the stack of pages.
        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            //check if fields have anything. If they dont, display an alert.
            //If they are full, send them off to be checked
            if (String.IsNullOrEmpty(Username.Text) || String.IsNullOrEmpty(Password.Text))
                await DisplayAlert("Login", "Username and password field can not be empty", "Ok");
            else
            {
                loginResponse = await restService.test_login(Username.Text, Password.Text);
                if (loginResponse.Status.Contains("true"))
                {
                    user = await restService.GetUserData(loginResponse.Email);
                    loginResponse = null; //delete the login response so that password is not stored anywhere
                    await DisplayAlert("Login", "Login Successful!", "Ok");
                    await Navigation.PushAsync(new HomePage(restService,user));
                }
                else
                {
                    await DisplayAlert("Login", "Username or password is incorrect", "Ok");
                }

            }
        }        
    }
}
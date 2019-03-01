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
	public partial class HelloXamlPage : ContentPage
	{
		public HelloXamlPage ()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                GoToLoginPage();

                return false; // True = Repeat again, False = Stop the timer
            });


        }

        //Loads login page after you tap on the image button (will try to get this to be the whole screen)
        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        //For the timer made a seperate function that opens login page after timer
        //The timer one didnt need arguments, so seperate function
        private async void GoToLoginPage()
        {
            await Navigation.PushAsync(new LoginPage());

        }
    }
}
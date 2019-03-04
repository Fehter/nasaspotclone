using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SPOT_App
{
	public partial class App : Application
	{
        static RestService rs;

		public App()
		{
			InitializeComponent();
            rs = new RestService();
            rs.TestPOST();

			//MainPage = new NavigationPage(new LoginPage()); // This causes the LoginPage to be the first thing the user sees.
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

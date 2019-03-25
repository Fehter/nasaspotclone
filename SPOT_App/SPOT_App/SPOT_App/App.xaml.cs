using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPOT_App.ViewModels; // This is necessary because the RequestViewModel class is defined in the "SPOT_App.ViewModels" namespace.
                           // Without this line, attempting to declare/initialize a RequestViewModel object would fail.

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SPOT_App
{
	public partial class App : Application
	{
        public RestService restService;

        // The following RequestViewModel is used to test the restService class.
        RequestViewModel requestViewModel;

        // This int is used to easily change the contents of each property of the RequestViewModel object (because it is appended onto the end of the content for a property).
        // I use this to be able to identify the content I just added to the database (as a result of a call to the RestService's test_setData() method).
        int x;

        public App()
		{
			InitializeComponent();

            x = 11;

            // Construct the test RequestViewModel object.
            requestViewModel = new RequestViewModel
            {
                Email = "123Email" + x,
                FirstName = "123FirstName" + x,
                LastName = "123LastName" + x,
                OrganizationName = "123OrgName" + x,                
                PrimaryPhoneNumber = "test PrimaryPhoneNumber " + x,
                AlternativePhoneNumber = "test AlternativePhoneNumber " + x,
                ContactTimes = "test ContactTimes " + x,
                PresentationLocation = "test PresentationLocation " + x,
                PresentationRequested = "test PresentationRequested " + x,
                PresentationRotations = "test PresentationRotations " + x,
                HandsOnActivity = "test HandsOnActivity " + x,
                GradeLevels = "test GradeLevels " + x,
                NumberOfStudents = "test NumberOfStudents " + x,
                ProposedDateAndTime = "test ProposedDateAndTime " + x,
                Supplies = "test Supplies " + x,
                TravelFee = "test TravelFee " + x,
                AmbassadorStatus = "test AmbassadorStatus " + x,
                OtherConcerns = "test OtherConcerns " + x,
                PresentationAlternativeMethod = "test PresentationAlternativeMethod " + x
            };

            // Construct the RestService object (thereby constructing the HttpClient object that the RestService has as a data member).
            restService = new RestService();

            // The following two lines are calls to the test functions of the RestService class.
            // NOTE: since these two functions are asynchronous operations, they will NOT necessarilly print their outputs into the console in the same order they were called.
            // I recommend calling the functions one at a time.
            //restService.test_getData();
            //restService.test_setData(requestViewModel);

            // Call the RestService's GetRequestData() with integer arguments 0 and 100 -- this means that the PHP file on the web server will query information for the first 100 requests in the database.
           // restService.GetRequestData(0, 100);

            // Call the RestService's test_login() function with the specified email and password arguments.
            // Note that "testuseremail1@test.com" and "password1" is an actual email/password combination in the SQL database (assuming that you used Kyle's latest script to create the "spot" database).
            //restService.test_login("testuseremail1@test.com", "password1");

            // The following test_login() calls will all fail (assuming you are using Kyle's database):
            //restService.test_login("testuseremail11@test.com", "password1");
            //restService.test_login("testuseremail1@test.com", "");
            //restService.test_login("", "password1");
            //restService.test_login("testuseremail1@test.com", "password11");

            // I've commented out the following line because, for testing connectivity, we do not need to construct any GUI related objects.
            MainPage = new NavigationPage(new TitlePage(restService)); // This causes the LoginPage to be the first thing the user sees.
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

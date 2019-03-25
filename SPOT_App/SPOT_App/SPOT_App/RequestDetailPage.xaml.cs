//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPOT_App.ViewModels; // We need this using statement because the RequestViewModel is defined in the test_xamarin_app.ViewModels namespace

namespace SPOT_App
{
    // This class defines how the "RequestDetailPage" (which is pushed onto the stack of pages when the user clicks on a request in the request list looks.
    // In RequestDetailPage's constructor, a RequestViewModel object is passed as an argument from the Handle_RequestTapped method of the RequestsPage.xaml.cs file.
    // This RequestViewModel is then set as the "BindingContext" of this page. This means that I can now implicitly bind properties of the that RequestViewModel to GUI components of this page.
    // For example, one line in the RequestDetailPage.xaml file reads:
    // <Label Text="{Binding OrganizationName}" FontSize="{StaticResource contentFontSize}" TextColor="{StaticResource contentTextColor}" HorizontalOptions="Start" VerticalOptions="Center"/>
    // The "Text="{Binding OrganizationName}" component of this line tells the compiler to pull the value of the OrganizationName property of the RequestViewModel.
    // This value is then used as the content of the Label's Text attribute.

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestDetailPage : ContentPage
	{
        //public RequestViewModel selectedRequestViewModel;

        public RequestDetailPage (RequestViewModel selectedRVM)
		{
			InitializeComponent();

            //selectedRequestViewModel = selectedRVM;

            // The following line allows us to implicitly bind to the data members of the RequestViewModel object "selectedRVM" in the corresponding xaml file "RequestDetailPage.xaml"
            BindingContext = selectedRVM; //selectedRequestViewModel;
        }

        async private void AcceptRequest_Button_Clicked(object sender, System.EventArgs e)
        {
            // The following DisplayAlert is a placeholder
            await DisplayAlert("Request Accepted", "You accepted this request", "OK");
        }
    }
}
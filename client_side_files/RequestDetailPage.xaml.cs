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
        RestService restService;
        RequestViewModel rvm;
        public RequestDetailPage (RequestViewModel selectedRVM)
        {
            InitializeComponent();

            BindingContext = selectedRVM;
        }

        public RequestDetailPage (RequestViewModel selectedRVM, RestService restService)
		{
			InitializeComponent();
            this.restService = restService;
            rvm = selectedRVM;
            System.Diagnostics.Debug.WriteLine("*****RequestDetailPage class and display rvmContents:********\n"+ rvm.GetContents());


            // The following line allows us to implicitly bind to the data members of the RequestViewModel 
            // object "selectedRVM" in the corresponding xaml file "RequestDetailPage.xaml"
            BindingContext = selectedRVM; //selectedRequestViewModel;


        }

        async private void AcceptRequest_Button_Clicked(object sender, System.EventArgs e)
        {
            // The following DisplayAlert is a placeholder
            System.Diagnostics.Debug.WriteLine("inside RequestDetailPage.xaml.cs , AcceptRequest_Button_Clicked(): PresentationID:" + rvm.PresentationID);
            bool hasTheRequestBeenAccepted = await restService.CheckAcceptanceOfRequest(rvm.PresentationID, rvm.Email);
            //Since false means that the request has not been accepted then accept the request.
            if (!hasTheRequestBeenAccepted)
            {
                //Do the php thing to accept the request.
                bool acceptRequest = await restService.AcceptRequest(rvm.PresentationID, rvm.Email);
                System.Diagnostics.Debug.WriteLine("acceptRequest:" + acceptRequest);
                if(acceptRequest)
                {
                    await DisplayAlert("Request Accepted", "You accepted this request", "OK");
                }
                    
                else
                {
                    await DisplayAlert("Request Declined.", "Sorry, but something went wrong trying to accept this request.", "Close");
                }
                    
            }
            else
            {
                //Reject the acceptance request.
                await DisplayAlert("Request Declined.", "Sorry, but the request was already accepted.", "Close");
            }
            
        }
    }
}
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPOT_App.ViewModels; // We need this using statement because the RequestViewModel is defined in the test_xamarin_app.ViewModels namespace
using System.Collections.Generic;
using System;

namespace SPOT_App
{
    // This class defines how the list of RequestViewModel objects will be shown in the GUI.
    // Temporarily, it constructs 20 RequestViewModel objects and stores them in an "ObservableCollection".
    // This ObservableCollection is used by the ListView "requests" (which is declared in the RequestsPage.xaml file NOT in this file -- 
    // I think it is also possible to define requests in this file, but I didn't have time to figure it out) to display the RequestViewModel objects.

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestsPage : ContentPage
    {
        //============================================================
        //Data Memebers
        public ObservableCollection<RequestViewModel> requestCollection { get; set; } 
        public RestService restService;
        private int firstNumOfRequests=0;

        //============================================================
        public RequestsPage(RestService restService)
        {
            InitializeComponent();
            this.restService = restService;
   
            constructRequestView();

           

        }
        private async void constructRequestView()
        {
            //List that stores all of the cells that is being displayed.
            requestCollection = new ObservableCollection<RequestViewModel> { }; // This variable will contain the RequestViewModel objects that store request data.

            //A list that contains all the data that needs to be displayed.
            List<RequestViewModel> rvmList= await restService.GetRequestData(10, firstNumOfRequests);
            
            //Filling out the cells using rvmList that is also being filled in inside this loop.
            foreach (RequestViewModel rvm in rvmList)
            {
                requestCollection.Add(new RequestViewModel
                {
                    PresentationID = rvm.PresentationID,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Name = rvm.FirstName + " " +rvm.LastName,
                    PresentationLocation = rvm.organization_street_address + " " + rvm.organization_city + " ," + rvm.organization_state + " " + rvm.organization_zip,
                    OrganizationName = rvm.OrganizationName,
                    Email = rvm.Email,
                    PrimaryPhoneNumber = rvm.PrimaryPhoneNumber,
                    HandsOnActivity = rvm.HandsOnActivity,
                    GradeLevels = rvm.GradeLevels,
                    NumberOfStudents = rvm.NumberOfStudents,
                    ProposedDateAndTime = rvm.ProposedDateAndTime,
                    Supplies = rvm.Supplies,
                    TravelFee = rvm.TravelFee,
                    PresentationRotations = rvm.PresentationRotations,
                    PreferredDays = rvm.PreferredDays,
                    OtherConcerns = rvm.OtherConcerns,
                    ContactTimes = rvm.ContactTimes,
                    AmbassadorStatus = rvm.AmbassadorStatus,
                    SubjectRequested = rvm.SubjectRequested,
                });


            }
            
            
            
            requests.ItemTemplate = new DataTemplate(typeof(RequestCell)); // The "RequestCell" defines how the RequestViewModel objects will be displayed by the GUI (the View).
            requests.HasUnevenRows = true;
            requests.SeparatorColor = Color.Black;
            requests.ItemsSource = requestCollection; // "requests" is the ListView defined in the RequestPage.xaml file -- the ItemsSource property tells this ListView where it should get information for its list items from.
            
            //This lane just make the list go back to the top. (mainly for when you press next and back)
            requests.ScrollTo(requestCollection[0], ScrollToPosition.Start, true);
            
        }
          
       

        async void Handle_RequestTapped(object sender, ItemTappedEventArgs e)
        {
           //sender.GetType();

            if (e.Item == null)
                return;
            
            //await DisplayAlert("Item Tapped", "An item was tapped." + "the type of the sender was: " + sender.GetType() + " " + sender.GetHashCode(), "OK");

            RequestViewModel selectedRequestViewModel = (RequestViewModel)((ListView)sender).SelectedItem; // This line lets me store the RequestViewModel object that was tapped by the user.

            //await DisplayAlert("x", "x is " + selectedRequestViewModel.GetType(), "ok");

            //await DisplayAlert("************", selectedRequestViewModel.GetContents(), "ok");

            await Navigation.PushAsync(new RequestDetailPage(selectedRequestViewModel, restService)); // Here I am passing the RequestViewModel object to the RequestDetailPage (this page will then display ALL the contents of the RequestViewModel object).

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        // The following class extends the ViewCell class to create a custom GUI for each RequestViewModel in the "requests" ListView.
        // Changing the contents of this class will affect how ALL RequestViewModels in the ListView are displayed.
        // This code -- most likely -- could have all been written in the RequestsPage.xaml file.
      
        //Function behind the "Back" button on the RequestsPage.xaml. It queries the db in increments of 10. 
        //When pressed: grabs and displays previous 10 requests in db.
        private void Back_Button_Clicked(object sender, EventArgs e)
        {
            //Making sure that it does not go under 0
            if (firstNumOfRequests - 10 >= 0)
            {
                firstNumOfRequests = firstNumOfRequests - 10;
                constructRequestView();
            }
        }
        //Function behind the "Next" button on the RequestsPage.xaml 
        //When pressed: grabs and displays the next 10 requests.
        private void Next_Button_Clicked(object sender, EventArgs e)
        {
            if(requestCollection.Count >= 10)
            {
                firstNumOfRequests = firstNumOfRequests + 10;
                constructRequestView();
            }
            else
            {
                DisplayAlert("End of the list.", "Sorry, but there are no more requests to display.", "Close");
            }
            
            
        }
        
       
    
        
    }
    public class RequestCell : ViewCell
        {
            public RequestCell()
            {
                var nameHeader = new Label();
                var organizationHeader = new Label();
                var locationHeader = new Label();

                var name = new Label();
                var organization = new Label();
                var location = new Label();

                var cellRow1 = new StackLayout();
                var cellRow2 = new StackLayout();
                var cellRow3 = new StackLayout();
                var mainLayout = new StackLayout();

                nameHeader.Text = "Name:";
                organizationHeader.Text = "Organization:";
                locationHeader.Text = "Location:";

                nameHeader.VerticalOptions = LayoutOptions.CenterAndExpand;
                nameHeader.HorizontalOptions = LayoutOptions.StartAndExpand;
                organizationHeader.VerticalOptions = LayoutOptions.CenterAndExpand;
                organizationHeader.HorizontalOptions = LayoutOptions.StartAndExpand;
                locationHeader.VerticalOptions = LayoutOptions.CenterAndExpand;
                locationHeader.HorizontalOptions = LayoutOptions.StartAndExpand;

                nameHeader.FontSize = 11;
                organizationHeader.FontSize = 11;
                locationHeader.FontSize = 11;

                //nameHeader.WidthRequest = 50;
                //organizationHeader.WidthRequest = 50;
                //locationHeader.WidthRequest = 50;

                name.SetBinding(Label.TextProperty, new Binding("Name"));
                organization.SetBinding(Label.TextProperty, new Binding("OrganizationName"));
                location.SetBinding(Label.TextProperty, new Binding("PresentationLocation"));

                name.FontSize = 10;
                organization.FontSize = 10;
                location.FontSize = 10;

                name.TextColor = Color.Black;
                organization.TextColor = Color.Black;
                location.TextColor = Color.Black;

                name.VerticalOptions = LayoutOptions.CenterAndExpand;
                name.HorizontalOptions = LayoutOptions.StartAndExpand;
                organization.VerticalOptions = LayoutOptions.CenterAndExpand;
                organization.HorizontalOptions = LayoutOptions.StartAndExpand;
                location.VerticalOptions = LayoutOptions.CenterAndExpand;
                location.HorizontalOptions = LayoutOptions.StartAndExpand;             

                cellRow1.Children.Add(nameHeader);
                cellRow1.Children.Add(name);
                cellRow2.Children.Add(organizationHeader);
                cellRow2.Children.Add(organization);
                cellRow3.Children.Add(locationHeader);                
                cellRow3.Children.Add(location);

                //labelLayout.Padding = 10;
                //labelLayout.VerticalOptions = LayoutOptions.FillAndExpand;
                //labelLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                //labelLayout.BackgroundColor = Color.Default;

                //mainLayout.Orientation = StackOrientation.Horizontal;

                mainLayout.Padding = 5;
                mainLayout.Spacing = 5;
                mainLayout.Children.Add(cellRow1);
                mainLayout.Children.Add(cellRow2);
                mainLayout.Children.Add(cellRow3);

                View = mainLayout; // This sets the View of the parent object (which I think is the ViewCell I am extending -- not quite sure yet).
            }
              
        }

}

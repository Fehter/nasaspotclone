//using System;
using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPOT_App.ViewModels; // We need this using statement because the RequestViewModel is defined in the test_xamarin_app.ViewModels namespace

namespace SPOT_App
{
    // This class defines how the list of RequestViewModel objects will be shown in the GUI.
    // Temporarily, it constructs 20 RequestViewModel objects and stores them in an "ObservableCollection".
    // This ObservableCollection is used by the ListView "requests" (which is declared in the RequestsPage.xaml file NOT in this file -- 
    // I think it is also possible to define requests in this file, but I didn't have time to figure it out) to display the RequestViewModel objects.

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestsPage : ContentPage
    {
        public ObservableCollection<RequestViewModel> requestCollection { get; set; }
        //public ListView requests;

        public RequestsPage()
        {
            InitializeComponent();
            
            requestCollection = new ObservableCollection<RequestViewModel> { }; // This variable will contain the RequestViewModel objects that store request data.

            // A simple for loop to construct 20 RequestViewModels with unique contents (see the + x on the end of each line).
            for (int x = 0; x < 20; x++)
            {
                requestCollection.Add(new RequestViewModel
                {
                    FirstName = "test FirstName " + x,
                    LastName = "test LastName " + x,
                    OrganizationName = "test OrganizationName " + x,
                    Email = "test Email " + x,
                    PrimaryPhoneNumber = "test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber " + x,
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
                });
            }

            requests.ItemTemplate = new DataTemplate(typeof(RequestCell)); // The "RequestCell" defines how the RequestViewModel objects will be displayed by the GUI (the View).
            requests.HasUnevenRows = true;
            requests.SeparatorColor = Color.Black;
            requests.ItemsSource = requestCollection; // "requests" is the ListView defined in the RequestPage.xaml file -- the ItemsSource property tells this ListView where it should get information for its list items from.           
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

            await Navigation.PushAsync(new RequestDetailPage(selectedRequestViewModel)); // Here I am passing the RequestViewModel object to the RequestDetailPage (this page will then display ALL the contents of the RequestViewModel object).

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        // The following class extends the ViewCell class to create a custom GUI for each RequestViewModel in the "requests" ListView.
        // Changing the contents of this class will affect how ALL RequestViewModels in the ListView are displayed.
        // This code -- most likely -- could have all been written in the RequestsPage.xaml file.
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
}

//using Xamarin.Forms;

namespace test_xamarin_app.ViewModels
{
    // This class defines an object that can be used to store all the information for a single request.
    // It is called "RequestViewModel" instead of "Request" because I was trying (perhaps unsuccessfully) to follow the View <-> ViewModel <-> Model design.
    // See this link for more information: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm#the-mvvm-pattern
    public class RequestViewModel
	{
        // As I understand it, these "get set" functions are used by the View of the MVVM pattern to be able to access the data members of this class.
        // Without them, it would not be possible to use something like:
        // <Label Text="{Binding OrganizationName}" FontSize="{StaticResource contentFontSize}" TextColor="{StaticResource contentTextColor}" HorizontalOptions="Start" VerticalOptions="Center"/>
        // in a .xaml file because the "{Binding OrganizationName}" would not be able to "get" the information.
        // Take this with some salt though -- I'm not comfortable with this yet.
        public string Name { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string ContactTimes { get; set; }
        public string PresentationLocation { get; set; } // This represents the School Address column
        public string PresentationRequested { get; set; }
        public string PresentationRotations { get; set; }
        public string HandsOnActivity { get; set; }
        public string GradeLevels { get; set; }
        public string NumberOfStudents { get; set; }
        public string ProposedDateAndTime { get; set; }
        public string Supplies { get; set; }
        public string TravelFee { get; set; } // This could be a boolean if the spreadsheet always contains either "Yes" or "No" for this column
        public string AmbassadorStatus { get; set; } // This relates to the "I understand that ambassadors are not ... and I take responsibility for them ..." column of the spreadsheet. This field should always say "Yes"
        public string OtherConcerns { get; set; }
        public string PresentationAlternativeMethod { get; set; } // This relates to the "Interested in alternative ways to receive a presentation?" column.

        public string GetContents() // Utility function to quickly get contents of all data members.
        {
            return
                Name + "\n" +
                OrganizationName + "\n" +
                Email + "\n" +
                PrimaryPhoneNumber + "\n" +
                AlternativePhoneNumber + "\n" +
                ContactTimes + "\n" +
                PresentationLocation + "\n" +
                PresentationRequested + "\n" +
                PresentationRotations + "\n" +
                HandsOnActivity + "\n" +
                GradeLevels + "\n" +
                NumberOfStudents + "\n" +
                ProposedDateAndTime + "\n" +
                Supplies + "\n" +
                TravelFee + "\n" +
                AmbassadorStatus + "\n" +
                OtherConcerns + "\n" +
                PresentationAlternativeMethod + "\n";
        }
    }   
}
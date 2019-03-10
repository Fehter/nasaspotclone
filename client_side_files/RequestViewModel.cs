//using Xamarin.Forms;
//using System.ComponentModel;
using System.Net.Http;
using System.Collections.Generic;

namespace SPOT_App.ViewModels
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

        // This function returns a "FormUrlEncodedContent" object constructed from an array of KeyValuePair objects.
        // Each KeyValuePair object is constructed from the name and contents of each get/set function of this RequestViewModel class.
        // The object returned by this function is then send (by the RestService class) as part of POST request to the PHP web service.
        public FormUrlEncodedContent GetFormUrlEncodedContent()
        {            
            // This is "shorthand" code that constructs/initializes the FormUrlEncodedContent object -- see the slightly-longer-but-clearer commented out equivalent code below if you are confused.
            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", this.Name),
                new KeyValuePair<string, string>("OrganizationName", this.OrganizationName),
                new KeyValuePair<string, string>("Email", this.Email),
                new KeyValuePair<string, string>("PrimaryPhoneNumber", this.PrimaryPhoneNumber),
                new KeyValuePair<string, string>("AlternativePhoneNumber", this.AlternativePhoneNumber),
                new KeyValuePair<string, string>("ContactTimes", this.ContactTimes),
                new KeyValuePair<string, string>("PresentationLocation", this.PresentationLocation),
                new KeyValuePair<string, string>("PresentationRequested", this.PresentationRequested),
                new KeyValuePair<string, string>("PresentationRotations", this.PresentationRotations),
                new KeyValuePair<string, string>("HandsOnActivity", this.HandsOnActivity),
                new KeyValuePair<string, string>("GradeLevels", this.GradeLevels),
                new KeyValuePair<string, string>("NumberOfStudents", this.NumberOfStudents),
                new KeyValuePair<string, string>("ProposedDateAndTime", this.ProposedDateAndTime),
                new KeyValuePair<string, string>("Supplies", this.Supplies),
                new KeyValuePair<string, string>("TravelFee", this.TravelFee),
                new KeyValuePair<string, string>("AmbassadorStatus", this.AmbassadorStatus),
                new KeyValuePair<string, string>("OtherConcerns", this.OtherConcerns),
                new KeyValuePair<string, string>("PresentationAlternativeMethod", this.PresentationAlternativeMethod)
            });           

            /*
            KeyValuePair<string, string>[] keyValuePairsArray =
            {
                new KeyValuePair<string, string>("Name", this.Name),
                new KeyValuePair<string, string>("OrganizationName", this.OrganizationName),
                new KeyValuePair<string, string>("Email", this.Email),
                new KeyValuePair<string, string>("PrimaryPhoneNumber", this.PrimaryPhoneNumber),
                new KeyValuePair<string, string>("AlternativePhoneNumber", this.AlternativePhoneNumber),
                new KeyValuePair<string, string>("ContactTimes", this.ContactTimes),
                new KeyValuePair<string, string>("PresentationLocation", this.PresentationLocation),
                new KeyValuePair<string, string>("PresentationRequested", this.PresentationRequested),
                new KeyValuePair<string, string>("PresentationRotations", this.PresentationRotations),
                new KeyValuePair<string, string>("HandsOnActivity", this.HandsOnActivity),
                new KeyValuePair<string, string>("GradeLevels", this.GradeLevels),
                new KeyValuePair<string, string>("NumberOfStudents", this.NumberOfStudents),
                new KeyValuePair<string, string>("ProposedDateAndTime", this.ProposedDateAndTime),
                new KeyValuePair<string, string>("Supplies", this.Supplies),
                new KeyValuePair<string, string>("TravelFee", this.TravelFee),
                new KeyValuePair<string, string>("AmbassadorStatus", this.AmbassadorStatus),
                new KeyValuePair<string, string>("OtherConcerns", this.OtherConcerns),
                new KeyValuePair<string, string>("PresentationAlternativeMethod", this.PresentationAlternativeMethod)
            };

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(keyValuePairsArray);
            */

            return formUrlEncodedContent;
        }

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
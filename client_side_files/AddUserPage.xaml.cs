using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOT_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUserPage : ContentPage
	{
        RestService rs;
		public AddUserPage (RestService rs)
		{
            this.rs = rs;
            InitializeComponent ();
		}
        private async void Add_User_Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Email.Text) || String.IsNullOrEmpty(Password.Text) || String.IsNullOrEmpty(FirstName.Text) ||
                String.IsNullOrEmpty(LastName.Text) || String.IsNullOrEmpty(PhoneNumber.Text))
                await DisplayAlert("Adding User", "Fields can't be empty. Please fill in user information.", "Ok");
            else if (!IsAdmin.IsToggled && !IsAmbassador.IsToggled && !IsTeacher.IsToggled)
                await DisplayAlert("Adding User", "A User must be a presenter, teacher, or administrator.", "Ok");
            else
                rs.AddUser(Email.Text, Password.Text, FirstName.Text, LastName.Text, PhoneNumber.Text, Convert.ToInt32(IsAmbassador.IsToggled).ToString(), Convert.ToInt32(IsTeacher.IsToggled).ToString(), Convert.ToInt32(IsAdmin.IsToggled).ToString());
        }
        private async void Cancel_Button_Clicked(object sender, EventArgs e) {
            await Navigation.PopToRootAsync();
        }
	}
}
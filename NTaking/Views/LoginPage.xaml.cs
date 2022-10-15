using NTaking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTaking.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
			//Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }

		private async void OnSignInClickedAsync(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

		private async void OnSignUpClickedAsync(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
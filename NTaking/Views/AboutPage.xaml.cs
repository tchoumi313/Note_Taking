using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTaking.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
		}

		async private void FaceBookButton_Clicked(object sender, EventArgs e)
		{
            await Browser.OpenAsync("https://wa.me/237695547004");
        }

		async private void GoogleButton_Clicked(object sender, EventArgs e)
		{
            await Browser.OpenAsync("mailto:tchouminzikeubd@gmail.com");
        }

		async private void WhatsAppButton_Clicked(object sender, EventArgs e)
		{
			await Browser.OpenAsync("https://wa.me/237695547004");
        }
    }
}
using NTaking.Models;
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
	public partial class HomeSessions : ContentPage
	{
		
		public HomeSessions ()
		{
			InitializeComponent ();
		}
		protected override async void OnAppearing()
				{
					base.OnAppearing();
			
					SessionsColView.ItemsSource = await App.MyDB.GetSessions();
				}
		private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var answer = await DisplayAlert("Question?",
				"What do you want to do", 
				"Add Invigilator", 
				"Change session info /Delete "
				
				);
            Session session = (Session)e.CurrentSelection.FirstOrDefault();

            if (answer)
			{
                //await DisplayAlert("alert", $"{session.Id}current session null homesession", "ok");
                await Shell.Current.GoToAsync($"{nameof(InvigilatorsPage)}?{nameof(InvigilatorsPage.SessionId)}={session.Id}");
			}
			else
			{
                if (e.CurrentSelection != null)
                {
                    // Navigate to NewSessionPage, passing the Id as a query parameter.
                    
                    //DisplayAlert("Session", $"{session.Id} id ,{session.Name},{session.Date} going to ", "ok");
                    await Shell.Current.GoToAsync($"{nameof(NewSessionPage)}?{nameof(NewSessionPage.SessionId)}={session.Id.ToString()}");
                }
                else
                {
                   await DisplayAlert("alert", "current session null homesession", "ok");
                }
            }
			

		}

		private async void OnAddClicked(object sender, EventArgs e)
		{
			// Navigate to the NewSessionPage
			//await Navigation.PushAsync(new NewSessionPage());
			await Shell.Current.GoToAsync(nameof(NewSessionPage));
        }
    }
}
using NTaking.Data;
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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SessionId), nameof(SessionId))]
    public partial class NewSessionPage : ContentPage
    {
        public string SessionId
        {

            set
            {
                LoadSession(value);
            }
        }
        public NewSessionPage()
        {
            InitializeComponent();
            BindingContext = new Session();
        }
        async void LoadSession(string itemId)
        {
            try
            {
                Session session = await App.MyDB.GetSession(Convert.ToInt32(itemId));
                //DisplayAlert("Session", $"{session.Name},{session.Date}", "ok");
                BindingContext = session;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load Session");
            }
        }

        async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            Session session = (Session)BindingContext;
            if (string.IsNullOrWhiteSpace(session.Name))
            {
                await DisplayAlert("Alert", "Fill in the session name please!!!", "Ok");
            }
            else
            {
                if (!(session.Id == 0))
                {
                    await App.MyDB.UpdateSession(session);
                }

                else
                {

                    Session Newsession = new Session
                    {
                        Name = session.Name,// + session.Date.ToShortDateString().ToString(),
                        Date = session.Date
                    };
                    await App.MyDB.CreateSession(Newsession);
                }
                await Shell.Current.GoToAsync("..");

            }
        }




        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

            Session session = (Session)BindingContext;

            // DisplayAlert("Session", $"{session.Id} id ,{session.Name},{session.Date}", "ok");

            App.MyDB.DeleteSession(session);
            
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        
    }
}
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
	[QueryProperty(nameof(InvigilatorId), nameof(InvigilatorId))]
    [QueryProperty(nameof(SessionId), nameof(SessionId))]
    public partial class NewInvigilatorPage : ContentPage
	{
        public int SessionId;

        public string InvigilatorId
		{
			set
			{
				LoadInvigilator(value);
			}
		}
		public NewInvigilatorPage ()
		{
			InitializeComponent ();
			BindingContext = new Invigilator();
		}
        public NewInvigilatorPage(string id)
        {
            InitializeComponent ();
            SessionId = Convert.ToInt32(id);
            BindingContext = new Invigilator();

        }

		async public void LoadInvigilator(string Id)
		{
            try
            {
                Invigilator invigilator = await App.MyDB.GetInvigilator(Convert.ToInt32(Id));
                SessionId = invigilator.SessionId;
                BindingContext = invigilator;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load Session");
            }
           
		}

		private async void OnCreateButtonClicked(object sender, EventArgs e)
		{
            Invigilator invigilator = (Invigilator)BindingContext;

           
                if (string.IsNullOrWhiteSpace(invigilator.FirstName) ||
                    string.IsNullOrWhiteSpace(invigilator.SecondName) ||
                    string.IsNullOrWhiteSpace(invigilator.PhoneNumber.ToString()) ||
                    string.IsNullOrWhiteSpace(invigilator.RoomNumber.ToString()) ||
                    string.IsNullOrWhiteSpace(invigilator.Observation) ||
                    string.IsNullOrWhiteSpace(invigilator.Function))
                {
                    await DisplayAlert("Alert", "To proceed,please Fill in all informations", "Ok");
                }
                else
                {
                    if (SessionId != 0)
                    {
                        Session session = await App.MyDB.GetSession(SessionId);
                        invigilator.Session = session;
                        invigilator.SessionId = session.Id;
                        //await DisplayAlert("invigilator", $"{invigilator.Id}", "ok");
                    if ((invigilator.Id != 0))
                        {
                        //await DisplayAlert("invigilator", $"{invigilator.Session.Name} updating", "ok");
                        await App.MyDB.UpdateInvigilator(invigilator);
                        }
                        else
                        {
                        //await DisplayAlert("invigilator", $"{invigilator.Id}", "ok");
                        

                        Invigilator NewInvi = new Invigilator
                            {
                                FirstName = invigilator.FirstName,
                                SecondName = invigilator.SecondName,
                                PhoneNumber = invigilator.PhoneNumber,
                                RoomNumber = invigilator.RoomNumber,
                                Observation = invigilator.Observation,
                                Function = invigilator.Function,

                                SessionId = invigilator.SessionId,
                                Session = invigilator.Session

                            };
                       // await DisplayAlert("invigilator", $"{NewInvi.Session.Name}", "ok");
                        await App.MyDB.CreateInvigilator(NewInvi);
                           
                        }
                    }
                    else
                    {
                    

                    }

                
                    await Shell.Current.GoToAsync("..");

                }
            
			
        }

        async private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            Invigilator invigilator = (Invigilator)BindingContext;

            // DisplayAlert("Session", $"{session.Id} id ,{session.Name},{session.Date}", "ok");

            await App.MyDB.DeleteInvigilator(invigilator);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

       
    }
}
using NTaking.Views;
using NTaking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NTaking.Data;
using NTaking.Services;

namespace NTaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SessionId),nameof(SessionId))]
	public partial class InvigilatorsPage : ContentPage
	{
        public string test ;
        private ExcelServices ExcelServices;

        public string SessionId
        {
            get
            {
                return SessionId;
            }
            set
            {
                test = value;
                LoadInvigilators(value);
            }
        }
		public InvigilatorsPage ()
		{
			InitializeComponent ();
            ExcelServices = new ExcelServices();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SessionsColView.ItemsSource = await App.MyDB.GetInvigilators(test);
            //SessionsColView.ItemsSource = await App.MyDB.GetInvigilators();
        }

        async public void LoadInvigilators(string Id)
        {
            try
            {
                //await DisplayAlert("Alert", $"{Id} LoadInvigikator execution", "ok");

                /*List<Invigilator> list = await App.MyDB.GetInvigilators(Id);
               
                SessionsColView.ItemsSource = list;*/
            }catch (Exception ex)
            {
                await DisplayAlert("Alert", "No invigilator loaded", "ok");
            }
        }
        
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                if (e.CurrentSelection != null)
                {
                // Navigate to NewSessionPage, passing the Id as a query parameter.
                Invigilator invigilator = (Invigilator)e.CurrentSelection.FirstOrDefault();
                    //DisplayAlert("Session", $"{session.Id} id ,{session.Name},{session.Date} going to ", "ok");
                    await Shell.Current.GoToAsync($"{nameof(NewInvigilatorPage)}?{nameof(NewInvigilatorPage.InvigilatorId)}={invigilator.Id.ToString()}");
                }
                else
                {
                    await DisplayAlert("alert", "current session null homesession", "ok");
                }
            


        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NewSessionPage
            //await Navigation.PushAsync(new NewSessionPage());
           // DisplayAlert("alert", $"{SessionId}   current session null homesession", "ok");
            int id =21;
            NewInvigilatorPage page = new NewInvigilatorPage(test);
            await Navigation.PushAsync(page);

           // await Shell.Current.GoToAsync(nameof(NewInvigilatorPage));
        }

        async private void OnExportClicked(object sender, EventArgs e)
        {
            Session session = await App.MyDB.GetSession(Convert.ToInt32(test));
            string date = session.Date.ToString("D");
            string fileName = $"{session.Name}_{date}.xlsx";
            List<Invigilator> Invigilators = await App.MyDB.GetInvigilators(test);
           
            await App.MyDB.ExportToExcel(ExcelServices, fileName, Invigilators, date);

            await DisplayAlert("alert", $"Exportation done", "ok");


        }

        async private void OnBestClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync($"{nameof(BestInvigilatorPage)}?{nameof(BestInvigilatorPage.SessionId)}={test}");
        }
    }
}
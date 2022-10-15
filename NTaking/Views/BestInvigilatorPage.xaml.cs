using NTaking.Data;
using NTaking.Models;
using NTaking.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTaking.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SessionId), nameof(SessionId))]
    public partial class BestInvigilatorPage : ContentPage
	{
        private ExcelServices ExcelServices;
       // public ObservableCollection<Invigilator> Invigilators;
		public BestInvigilatorPage ()
		{
			InitializeComponent ();
            ExcelServices = new ExcelServices ();   
		}
        public string test;
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
       

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Invigilator> invigilators = await App.MyDB.GetInvigilators(test);
            List<Invigilator> invigs = new List<Invigilator>();
            foreach (Invigilator invigilator in invigilators)
            {
                if (invigilator.Observation.StartsWith("Bien"))
                {
                    invigs.Add(invigilator);    
                }
            }
            
            SessionsColView.ItemsSource  = invigs;
            //SessionsColView.ItemsSource = await App.MyDB.GetInvigilators();
        }

        async public void LoadInvigilators(string Id)
        {
            try
            {
                //await DisplayAlert("Alert", $"{Id} LoadInvigikator execution", "ok");

                /*List<Invigilator> list = await App.MyDB.GetInvigilators(Id);
               
                SessionsColView.ItemsSource = list;*/
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "No invigilator loaded", "ok");
            }
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

          /*  if (e.CurrentSelection != null)
            {
                // Navigate to NewSessionPage, passing the Id as a query parameter.
                Invigilator invigilator = (Invigilator)e.CurrentSelection.FirstOrDefault();
                //DisplayAlert("Session", $"{session.Id} id ,{session.Name},{session.Date} going to ", "ok");
                await Shell.Current.GoToAsync($"{nameof(NewInvigilatorPage)}?{nameof(NewInvigilatorPage.InvigilatorId)}={invigilator.Id.ToString()}");
            }
            else
            {
                await DisplayAlert("alert", "current session null homesession", "ok");
            }*/



        }

       

        async private void OnExportClicked(object sender, EventArgs e)
        {
            Session session = await App.MyDB.GetSession(Convert.ToInt32(test));
            string date = session.Date.ToString("D");
            string fileName = $"Meilleur de la {session.Name}_{date}.xlsx";
            List<Invigilator> invigs = await App.MyDB.GetInvigilators(test);
            List<Invigilator> Invigilators = new List<Invigilator>();
            foreach (Invigilator invigilator in invigs)
            {
                if (invigilator.Observation.StartsWith("Bien"))
                {
                    Invigilators.Add(invigilator);
                }
            }
            await App.MyDB.ExportToExcel(ExcelServices,fileName, Invigilators, date);
            await DisplayAlert("alert", $"Exportation done", "ok");


        }

    }
}
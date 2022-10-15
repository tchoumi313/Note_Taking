
using NTaking.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NTaking
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewSessionPage), typeof(NewSessionPage));
            Routing.RegisterRoute(nameof(NewInvigilatorPage), typeof(NewInvigilatorPage));
            //string id =null;
            //NewInvigilatorPage newInvigilatorPage = new NewInvigilatorPage(id);
           // Routing.RegisterRoute(nameof(newInvigilatorPage), typeof(NewInvigilatorPage));
            Routing.RegisterRoute(nameof(InvigilatorsPage), typeof(InvigilatorsPage));
            Routing.RegisterRoute(nameof(BestInvigilatorPage), typeof(BestInvigilatorPage));
        }

       /* private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }*/
    }
}

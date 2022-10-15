using NTaking.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using NTaking.Views;

namespace NTaking
{
    public partial class App : Application
    {
        static SQLHelper db;
        public static SQLHelper MyDB
        {
            get
            {
                if(db == null)
                {
                    db = new SQLHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "NoteTaking_SDPSR.db3"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

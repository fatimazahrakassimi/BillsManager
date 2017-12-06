//using BillsMaanger.View;
using System;
using BillsManager.View;
using Xamarin.Forms;
using BillsManager.Model;
using BillsManager.service;
using Xamarin.Forms.Xaml;


namespace BillsManager
{
    public class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        static FactureServiceImpl database;
      

        public App()
        {
           // MainPage = new NavigationPage(new BillsPage());
           
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new ListeFactureCS());
            }
           
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));
            }
        
        public static FactureServiceImpl Database
        {
            get
            {
                if(database== null)
                {
                    database = new FactureServiceImpl("StrategieRecouvrement.db3");
                }
                return database;

            }
        }

       

        public int ResumeAtListeFactId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}


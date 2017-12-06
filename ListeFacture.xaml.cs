using BillsManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace BillsManager.View
{
    public partial class ListeFacture : ContentPage
    {

        public ListeFacture()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtListeFactId = -1;
            //listView.ItemsSource = await App.Database.getFactureAsync();
        }

        async void OnLogOut(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage
            {
                BindingContext = new LoginPage()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtListeFactId = (e.SelectedItem as Facture).id;
            Debug.WriteLine("setting ResumeAtListeFactId = " + (e.SelectedItem as Facture).id);

            await Navigation.PushAsync(new DetailFacture
            {
                BindingContext = e.SelectedItem as Facture
            });
        }
    }
}
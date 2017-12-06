using BillsManager;
using BillsManager.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BillsManager.View
{

    public class ListeFactureCS : ContentPage
    {
        public static ObservableCollection<string> items { get; set; }
        public static Task<List<Facture>> listFact { get; set; }
        FactureServiceImpl factures;
        DataAccess database ;

        public ListeFactureCS()
        {
            database = new DataAccess();
            Title = "La liste des factures";
            var toolbarItem = new ToolbarItem
            {
                Text = "Log out"

            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };
            ToolbarItems.Add(toolbarItem);
            //items = database.Bills;
            items = new ObservableCollection<string>() { "FT14785", "AP789652", "Q4756982", "FL45632", "AE14526", "EA145269", "AF123698", "AZ1458963", "FD452631", "FD74159632" };
            //listFact = factures.getFactureAsync();
            ListView lstView = new ListView();
            lstView.IsPullToRefreshEnabled = true;
            lstView.Refreshing += OnRefresh;
            lstView.ItemSelected += OnSelection;
            lstView.ItemTapped += OnTap;
            lstView.ItemsSource = items;
            var temp = new DataTemplate(typeof(textViewCell));
            lstView.ItemTemplate = temp;
            Content = lstView;
            BackgroundColor = Color.Beige;

        }
        

        void OnTap(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("La facture selectionnée", e.Item.ToString(), "Ok");
        }


        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new DetailFacture() { BindingContext = e.SelectedItem as Facture });
            }
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            var itemList = items.Reverse().ToList();
            items.Clear();
            foreach (var s in itemList)
            {
                items.Add(s);
            }
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }


    }

    public class textViewCell : ViewCell
    {
        public textViewCell()
        {
            StackLayout layout = new StackLayout();
            layout.Padding = new Thickness(15, 0);
            Label label = new Label();

            label.SetBinding(Label.TextProperty, ".");
            layout.Children.Add(label);

            var moreAction = new MenuItem { Text = "More" };
            moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            moreAction.Clicked += OnMore;

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += OnDelete;

            this.ContextActions.Add(moreAction);
            this.ContextActions.Add(deleteAction);
            View = layout;
        }

        void OnMore(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            //Do something here... e.g. Navigation.pushAsync(new specialPage(item.commandParameter));
            //page.DisplayAlert("More Context Action", item.CommandParameter + " more context action", 	"OK");
        }

        void OnDelete(object sender, EventArgs e)
        {
            var item = (string)sender;
            ListeFactureCS.items.Remove(item);
        }

    }
}






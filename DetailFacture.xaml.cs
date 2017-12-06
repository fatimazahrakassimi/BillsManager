using System;
using System.Reflection;
using Xamarin.Forms;
using System.Collections.Generic;
using BillsManager.service;

namespace BillsManager.View
{
    public partial class DetailFacture : ContentPage
    {
        public DataAccess database;
        List<string> modePaiement = new List<string>
        {
            { "Cheque" }, { "Especes" },
            { "Effet" }, { "Bon cacheté" }
        };
        public DetailFacture()
        {
            Title = "Détail de la facture";
            var toolbarItem = new ToolbarItem
            {
               
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                //DisplayAlert("les modifications ont été enregistrés avec succées", "Ok");
                await Navigation.PushAsync(new ListeFactureCS());
            };
            ToolbarItems.Add(toolbarItem);

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Détail de la Facture")
                {
                    new TableSection()
                    {
                        new EntryCell
                        {
                            Label = "Facture",
                            Height= 300,
                            Text = "FD147852",
                            IsEnabled=false

                        },
                        new EntryCell
                        {
                            Label = "Client",
                            Text="Fati",
                            IsEnabled=false
                        },
                        new EntryCell
                        {
                            Label = "Date d'échéance",
                            Height= 300,
                            Text="27/08/2017",
                            IsEnabled=false
                        },
                        new EntryCell
                        {
                            Label = "Montant",
                            Text="1456982.3",
                            Height= 300,
                            IsEnabled=false
                        },
                        new SwitchCell
                        {
                            Text = "Payée"
                        },
                        new EntryCell
                        {
                            Label = "Motif de retard",
                            Height= 300,
                            Placeholder = "Veuillez saisir la raison du retard du paiement"
                        },
                        new TextCell
                        {
                            Text="La date de réglement",
                            Height= 300,
                        },
                         new ViewCell
                        {
                             //Label = "Date d'échéance réelle",
                            View = new DatePicker
                            {

                                Format = "D",
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            }
                        },
                         new TextCell
                        {
                            Text="Mode de paiement"
                        },
                        new ViewCell
                        {
                            View=  new Picker
                            {
                                Title="Selectionner le mode de paiement",
                                ItemsSource = modePaiement,
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            }
                        }

        }
                }
                
            };
            Button button = new Button
            {
                Text = "Sauvegarder",
                BackgroundColor = Color.Green,
            };

            button.Clicked += (object sender, EventArgs e)=>
           {
               Save_Clicked(sender, e);

           };

        


            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                   
                    tableView,button
                }
            };
        }
        public void Save_Clicked(object sender, EventArgs e)
        {
            //var factItem = (Facture) sender ;
            //database.SaveBill(factItem);
            Navigation.PopAsync();
            //Navigation.PushAsync(new ListeFactureCS());

        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs args)
        {
            
            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex == -1)
                return;
            string selectedItem = picker.Items[selectedIndex];
            PropertyInfo propertyInfo = typeof(Keyboard).GetRuntimeProperty(selectedItem);
            
        }
    }
}

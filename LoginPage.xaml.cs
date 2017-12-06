using System;
using BillsManager.Model;
using BillsManager.View;
using Xamarin.Forms;
using BillsManager.service;
using System.Linq;

namespace BillsManager
{
    public class LoginPage : ContentPage
    {
        Entry usernameEntry, passwordEntry;
        DataAccess database;
        Label messageLabel;
        public LoginPage()
        {
            database = new DataAccess();
            var toolbarItem = new ToolbarItem
            {
                Icon = Device.OnPlatform(null, "zinecapitalinvest.png", "zinecapitalinvest.png")
            };
            //toolbarItem.Clicked += OnSignUpButtonClicked;
            ToolbarItems.Add(toolbarItem);

            messageLabel = new Label();
            usernameEntry = new Entry
            {
                Placeholder = "Nom d'utilisateur"
            };
            passwordEntry = new Entry
            {
                IsPassword = true,
                Placeholder = "mot de passe"
            };
            var loginButton = new Button
            {
                Text = "Login"
            };
            loginButton.Clicked += OnLoginButtonClicked;

            Title = "Login";
            Icon = "ressources/zine_capital_invest_.png";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Nom d'utilisateur",FontSize=20,TextColor=Color.Green },
                    usernameEntry,
                    new Label { Text = "Mot de passe",FontSize=20,TextColor=Color.Green },
                    passwordEntry,
                    loginButton,
                    
                }
            };
        }
        /*
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPageCS());
        }
        */
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new Utilisateur
            {
                UserName = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new ListeFactureCS(), this);
                await Navigation.PopAsync();
            }
            else
            {
                usernameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
                await DisplayAlert("Authentification échouée", "Nom d'utilisateur ou mot de passe incorrect", "Réessayer");
            }
        }

        bool authentification(Utilisateur user)
        {
            return database.Users.Contains(user);
        }

        bool AreCredentialsCorrect(Utilisateur user)
        {
            
            return user.UserName == Constants.Username && user.Password == Constants.Password;
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using BillsManager.Model;
using BillsManager.View;

using Xamarin.Forms;

namespace BillsManager
{
    public class SignUpPageCS : ContentPage
    {

        Entry usernameEntry, passwordEntry;
        Label messageLabel;


        public SignUpPageCS()
        {
            messageLabel = new Label();
            usernameEntry = new Entry
            {
                Placeholder = "username"
            };
            passwordEntry = new Entry
            {
                IsPassword = true
            };

            var signUpButton = new Button
            {
                Text = "Sign Up"
            };
            signUpButton.Clicked += OnSignUpButtonClicked;

            Title = "Sign Up";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Username" },
                    usernameEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    signUpButton,
                    messageLabel
                }
            };
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new Utilisateur()
            {
                UserName = usernameEntry.Text,
                Password = passwordEntry.Text,

            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }

        bool AreDetailsValid(Utilisateur user)
        {
            return (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password));
        }
    }

}



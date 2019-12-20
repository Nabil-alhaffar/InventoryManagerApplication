using System;
using System.Collections.Generic;
using InventoryManager.ViewModels;
using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class WelcomePage : ContentPage
    {
        WelcomePageViewModel viewModel;
        public WelcomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WelcomePageViewModel();
   

        }
    }
}

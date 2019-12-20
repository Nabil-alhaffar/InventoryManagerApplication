using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.ViewModels;

using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class PastOrdersPage : ContentPage
    {
        PastOrdersViewModel viewModel;

        public PastOrdersPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PastOrdersViewModel();

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Order;
            if (item == null)
                return;

            await Navigation.PushAsync(new PastOrdersDetailPage(new PastOrdersDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Orders.Count == 0)
                viewModel.LoadOrdersCommand.Execute(null);
        }
    }
}



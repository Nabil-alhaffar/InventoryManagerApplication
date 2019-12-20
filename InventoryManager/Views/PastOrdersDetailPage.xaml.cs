using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;
using InventoryManager.ViewModels;

using Xamarin.Forms;

namespace InventoryManager.Views
{

    public partial class PastOrdersDetailPage : ContentPage
    {
        PastOrdersDetailViewModel viewModel;
        public PastOrdersDetailPage(PastOrdersDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

        }
        public PastOrdersDetailPage() {
            InitializeComponent();

            var order = new Order

            {
                //Text = "Item 1",
               
            };

            viewModel = new PastOrdersDetailViewModel(order);
            BindingContext = viewModel;

        }
    }
}

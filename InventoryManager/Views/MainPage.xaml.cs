using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;

namespace InventoryManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.WelcomePage, (NavigationPage)Detail);
            //MenuPages.Add((int)MenuItemType.ProductManager, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.WelcomePage:
                        MenuPages.Add(id, new NavigationPage(new WelcomePage()));
                        break;
                    case (int)MenuItemType.ProductManager:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    
                    case (int)MenuItemType.Reconcile:
                        MenuPages.Add(id, new NavigationPage(new InventoryReconciliationPage()));
                        break;
                    case (int)MenuItemType.CurrentStoreStocks:
                        MenuPages.Add(id, new NavigationPage(new CurrentInventoryPage()));
                        break;
                    case (int)MenuItemType.InventoryTransfers:
                        MenuPages.Add(id, new NavigationPage(new InventoryTransfersPage()));
                        break;
                    case (int)MenuItemType.PastOrders:
                        MenuPages.Add(id, new NavigationPage(new PastOrdersPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;

                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InventoryManager.Services;
using InventoryManager.Views;
using System.IO;
using InventoryManager.Models;
namespace InventoryManager
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        //public static string AzureBackendUrl =
        //    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" :
        //    "http://localhost:5000";
        public static string AzureBackendUrl= "https://inventorymanager-mobileappservice.azurewebsites.net";
        public static string libPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library", "data");
        public static string sqliteURL = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library", "data","database.sqlite");
        public static bool UseMockDataStore = false;
        public static User currentUser { get; set; }
        public static Store currentStore{ get; set; }
        public App()
        {
            InitializeComponent();
            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore>();
                DependencyService.Register<UserMockDataStore>();
                DependencyService.Register<StoreMockDataStore>();
            }
            else
            {

                DependencyService.Register<AzureDataStore>();
                DependencyService.Register<AzureUserDataStore>();
                DependencyService.Register<AzureStoreDataStore>();
                DependencyService.Register<AzureStockDataStore>();
                DependencyService.Register<AzureSerialNumbersDataStore>();
                DependencyService.Register<AzureOrderDataStore>();
                DependencyService.Register<AzureOrderItemDataStore>();


            }
            currentUser = new User();

        MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (!Directory.Exists(libPath)) {
                Directory.CreateDirectory(libPath);
            }
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

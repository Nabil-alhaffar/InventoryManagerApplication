using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using InventoryManager.Models;
using InventoryManager.Services;

namespace InventoryManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Product> DataStore => DependencyService.Get<IDataStore<Product>>() ?? new MockDataStore();
        public IUserDataStore<User> UserDataStore => DependencyService.Get < IUserDataStore<User>>() ?? new UserMockDataStore();
        public IStoreDataStore<Store,Stock> StoreDataStore => DependencyService.Get<IStoreDataStore<Store, Stock>>() ?? new StoreMockDataStore();
        public IStockDataStore<Stock> StockDataStore => DependencyService.Get<IStockDataStore<Stock>>();
        public ISerialNumbersDataStore<SerialNumber> SerialNumbersDataStore => DependencyService.Get<ISerialNumbersDataStore<SerialNumber>>();
        public IOrderDataStore<Order> OrderDataStore => DependencyService.Get<IOrderDataStore<Order>>();



        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

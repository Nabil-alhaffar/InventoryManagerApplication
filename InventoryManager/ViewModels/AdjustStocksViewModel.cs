using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using InventoryManager.Models;
using Xamarin.Forms;
using InventoryManager.Views;
namespace InventoryManager.ViewModels
{

    public class AdjustStocksViewModel : BaseViewModel
    {
        public ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
        public AdjustStocksViewModel()
        {
            Title = "Add Stocks";
            Stocks = new ObservableCollection<Stock>();

            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AddStocksView, IEnumerable<Stock>>(this, "AddNonSerialStocks", (Action<AddStocksView, IEnumerable<Stock>>)(async (obj, stocks) =>
            {
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.AddStocksAsync(App.currentStore.StoreId, (IEnumerable<Stock>)newstocks);

            }));
        }

    }
}
//        public ObservableCollection<StocksGroupModel> StocksList { get; set; } = new ObservableCollection<StocksGroupModel>();

//        public AddStocksViewModel() {

//            StocksList = new ObservableCollection<StocksGroupModel>();
//        }
//        public IList<Stock> stocks { get; set;  }
//        public IList<SerialNumber> serialNumbers { get; set; }
//        public IList<Stock> Stocks
//        {
//            get { return stocks; }
//            set { stocks = value; }
//        }


//        public IList<SerialNumber> SerialNumbers {
//            get { return serialNumbers; }
//            set { serialNumbers = value; }

//        }

//        public int ProductId { get; set; }

//        public ICommand ProductIdChanged {
//            get
//            {
//                return GetProduct;
//            }

//        }




//        public bool IsProductAPhone { get; set; } 
//        //public Stock  Stock {get;set;}
//        public Product Product { get; set; } 
//        public ICommand GetProduct
//        {
//            get
//            {
//                return new Command(async () =>  
//                {
//                    try
//                    {
//                        Product = await DataStore.GetProductAsync(ProductId);
//                    }
//                    catch
//                    {


//                    }
//                    finally
//                    {
//                        if (Product != null)
//                            IsProductAPhone = Product.ProductType == "Phone";
//                    }
//                    //if (isPhone)
//                    //serialListView.isVisible = true;
//                    //     Application.Current.MainPage = new NavigationPage(new MenuPage()); 
//                    //Application.Current.MainPage = new MainPage();

//                });
//            }

//        }
//    }
//}


////        private ObservableCollection<SerialNumberViewModel> serialNumbers = new ObservableCollection<SerialNumberViewModel>();

////        public event PropertyChangedEventHandler PropertyChanged;

////        public AddStocksViewModel(Stock stock, book expanded = false)
////        {


////            Stocks = new ObservableCollection<Stock>();
////            MessagingCenter.Subscribe<IEnumerable<Stock>>(this, "AddStocks", async (stocks) =>
////            {
////                int storeId = stocks.First().StoreId;
////                foreach (Stock stock in stocks)
////                {
////                    var newStock = stock as Stock;
////                    Stocks.Add(stock);
////                }
////                await StoreDataStore.AddStocksAsync(storeId, stocks);

////            });
////        }
////        public string StateIcon
////        {
////            get
////            {
////                if (Expanded)
////                {
////                    return "arrow_a.png";
////                }
////                else
////                {
////                    return "arrow_b.png";
////                }
////            }
////        }
////        public int StoreId
////        {
////            get
////            {
////                return Stock.StoreId;
////            }
////        }
////        public int Quantity
////        {

////            get
////            {
////                return Stock.Quantity;
////            }
////        }
////        public Stock Stock
////        {
////            get;
////            set;
////        }
////    }
////}
////public class HotelViewModel : ObservableRangeCollection<RoomViewModel>, INotifyPropertyChanged
////{
////    // It's a backup variable for storing CountryViewModel objects  
////    private ObservableRangeCollection<RoomViewModel> hotelRooms = new ObservableRangeCollection<RoomViewModel>();
////    public HotelViewModel(Hotel hotel, bool expanded = false)
////    {
////        Hotel = hotel;
////        _expanded = expanded;
////        foreach (Room room in hotel.Rooms)
////        {
////            Add(new RoomViewModel(room));
////        }
////        if (expanded) AddRange(hotelRooms);
////    }
////    public HotelViewModel() { }
////    private bool _expanded;
////    public bool Expanded
////    {
////        get
////        {
////            return _expanded;
////        }
////        set
////        {
////            if (_expanded != value)
////            {
////                _expanded = value;
////                OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
////                OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
////                if (_expanded)
////                {
////                    AddRange(hotelRooms);
////                }
////                else
////                {
////                    Clear();
////                }
////            }
////        }
////    }
////    public string StateIcon
////    {
////        get
////        {
////            if (Expanded)
////            {
////                return "arrow_a.png";
////            }
////            else
////            {
////                return "arrow_b.png";
////            }
////        }
////    }
////    public string Name
////    {
////        get
////        {
////            return Hotel.Name;
////        }
////    }
////    public Hotel Hotel
////    {
////        get;
////        set;
////    }
////}
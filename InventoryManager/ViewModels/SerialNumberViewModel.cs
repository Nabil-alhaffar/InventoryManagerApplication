using System;
using InventoryManager.Models;
namespace InventoryManager.ViewModels
{
    public class SerialNumberViewModel: BaseViewModel
    {
        private SerialNumber _serialNumber;
        public SerialNumberViewModel(SerialNumber serialNumber)
        {
            _serialNumber = serialNumber;
        }
        public string SerialNumberValue
        {
            get
            {
                return _serialNumber.SerialNumberValue;
            }
        }
        public int StoreId
        {
            get
            { 
                return _serialNumber.StoreId;
            }
        }
        public int ProductId
        {
            get
            {
                return _serialNumber.ProductId;
            }

        }
        public SerialNumber SerialNumber
        {
            get => _serialNumber;
        }

    }
}

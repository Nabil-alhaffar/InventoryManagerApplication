using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface ISerialNumberRepository
    {
        void Add(SerialNumber serialNumber);
        void Update(SerialNumber serialNumber);
        SerialNumber Remove(string imeiNumberValue);
        SerialNumber Get(string imeiNumberValue);
        IEnumerable<SerialNumber> GetAll();
        int GetStoreFromSerialNumber(string imeiNumberValue) ;
    }
}

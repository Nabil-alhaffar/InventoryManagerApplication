using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte IsActive { get; set; }
        public int StoreId { get; set; }
        public string UserType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string UserName { get; set; }
        public string ZipCode { get; set; }
        public string Token { get; set; }

        public virtual Stores Store { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}

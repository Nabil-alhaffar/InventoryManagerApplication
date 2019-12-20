using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace InventoryManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int StoreId { get; set; }
        public string IsActive { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //public virtual Product Product { get; set; }

    }
    public enum UserType { SalesAssociate, StoreManager, DistrictManager, Admin }

}




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManager.MobileAppService.Models
{
    public partial class User
    {
        public User (){

            Orders = new HashSet<Order>();
            }
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        [Required]
        public byte IsActive { get; set; }
        [Required]
        public string UserType { get; set; }

        public string Token { get; set; }



        //public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }

    public enum UserType { SalesAssociate, StoreManager, DistrictManager, Admin }

}



using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.MobileAppService.Models
{
    public class AuthenticationModel
    {
        
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string StoreId { get; set; }
        
    }
}

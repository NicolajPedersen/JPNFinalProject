using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JPNFinalProject.Data.DTO;


namespace JPNFinalProject.Models.OrderViewModels {
    public class DeliveryViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Street { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string ParcelPickup { get; set; }

        public List<BusinessDTO> Businesses { get; set; }
    }
}

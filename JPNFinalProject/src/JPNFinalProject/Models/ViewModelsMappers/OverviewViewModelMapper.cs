using JPNFinalProject.Data.DTO;
using JPNFinalProject.Models.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.ViewModelsMappers
{
    public static class OverviewViewModelMapper
    {
        public static OverviewViewModel DeliveryViewModelToOverviewViewModel(DeliveryViewModel delivery) {
            OverviewViewModel model = new OverviewViewModel();
            model.Order = new OrderDTO() {
                //OrderNumber = Convert.ToInt32(Guid.NewGuid().ToString()),
                OrderNumber = 11111,
                CustomerMail = "",
                Person = new PersonDTO() {
                    Id = 1,
                    FirstName = delivery.Name,
                    LastName = delivery.Name,
                    Email = delivery.Email,
                    Password = "",
                    Phone = delivery.Phone,
                    Type = "",
                    Address = new AddressDTO() {
                        Id = 1,
                        Address = delivery.Street,
                        ZipCode = delivery.Zip,
                        City = delivery.City,
                        Country = "Denmark"
                    }
                },
                Business = new BusinessDTO() {
                    Id = Convert.ToInt32(delivery.ParcelPickup)
                }

            };


            return model;
        }
    }
}

using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class AddressMapper
    {
        public static AddressDTO AddressToAddressDTO(Address input) {
            return new AddressDTO() {
                Id = input.AddressId,
                Address = input.Street,
                ZipCode = input.PostalCode,
                City = input.City,
                Country = input.Country
            };
        }

        public static Address AddressDTOToAddress(AddressDTO dto) {
            return new Address() {
                Street = dto.Address,
                PostalCode = dto.ZipCode,
                City = dto.City,
                Country = dto.Country
            };
        }
    }
}

using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class BusinessMapper
    {
        public static BusinessDTO BusinessToBusinessDTO(Business input) {
            return new BusinessDTO() {
                Id = input.BusinessId,
                Name = input.Name,
                Address = AddressMapper.AddressToAddressDTO(input.Address),
                Phone = input.Phone,
                Email = input.Email,
                OperationalHour = input.OperationalHour
            };
        }
    }
}

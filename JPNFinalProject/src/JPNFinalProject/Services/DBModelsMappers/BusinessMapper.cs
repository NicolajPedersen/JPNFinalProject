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

        public static BusinessDTO BusinessToBusinessDTOV2(Business input)
        {
            BusinessDTO dto = new BusinessDTO();
            dto.Id = input.BusinessId;
            dto.Name = input.Name;
            if(input.Address != null)
            {
                dto.Address = AddressMapper.AddressToAddressDTO(input.Address);
            }
            dto.Phone = input.Phone;
            dto.Email = input.Email;
            dto.OperationalHour = input.OperationalHour;

            return dto;
        }

        public static Business BusinessDTOToBusiness(BusinessDTO dto) {
            return new Business() {
                Name = dto.Name,
                Address = AddressMapper.AddressDTOToAddress(dto.Address),
                Phone = dto.Phone,
                Email = dto.Email,
                OperationalHour = dto.OperationalHour
            };
        }
    }
}

using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class PersonMapper
    {
        public static Person PersonDTOToPerson(PersonDTO dto) {
            return new Person() {
                Name = dto.FirstName + " " + dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone,
                Type = dto.Type,
                //Address = AddressMapper.AddressDTOToAddress(dto.Address)
            };
        }

        public static PersonDTO PersonToPersonDTO(Person input) {
            return new PersonDTO() {
                FirstName = input.Name,
                LastName = input.Name,
                Email = input.Email,
                Password = input.Password,
                Phone = input.Phone,
                Type = input.Type,
                Address = AddressMapper.AddressToAddressDTO(input.Address)
            };
        }
    }
}

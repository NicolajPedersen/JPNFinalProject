using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class OrderMapper
    {
        public static Order OrderDTOToOrder(OrderDTO dto, int personId) {
            return new Order() {
                PersonId = personId,
                TotalPrice = Convert.ToDecimal(dto.TotalPrice),
                DeliveryTime = new DateTimeOffset()
            };
        }

        public static OrderDTO OrderToOrderDTO(Order input) {
            return new OrderDTO() {
                Person = PersonMapper.PersonToPersonDTO(input.Person),
                CustomerMail = "",
                Business = BusinessMapper.BusinessToBusinessDTO(input.BusinessOrder.Where(x => x.OrderId == input.OrderId).Select(x => x.Business).Single()),
                Products = DBProductMapper.ProductsToListOfProductDTOs(input.OrderProduct.Where(x => x.OrderId == input.OrderId).Select(x => x.Product).ToList()),
                TotalPrice = Convert.ToInt32(input.TotalPrice)
            };
        }
    }
}

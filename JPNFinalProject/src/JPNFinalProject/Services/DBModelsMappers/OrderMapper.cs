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
                CustomerMail = input.Person.Email,
                Business = BusinessMapper.BusinessToBusinessDTO(input.BusinessOrder.Where(x => x.OrderId == input.OrderId).Select(x => x.Business).Single()),
                Products = DBProductMapper.ProductsToListOfProductDTOs(input.OrderProduct.Where(x => x.OrderId == input.OrderId).Select(x => x.Product).ToList()),
                TotalPrice = Convert.ToInt32(input.TotalPrice)
            };
        }

        public static OrderDTO OrderToOrderDTOV2(Order input, List<OrderProduct> orderProducts)
        {
            OrderDTO dto = new OrderDTO();
            dto.Id = input.OrderId;
            dto.Person = PersonMapper.PersonToPersonDTO(input.Person);
            dto.CustomerMail = dto.Person.Email;
            dto.TotalPrice = Convert.ToInt32(input.TotalPrice);
            dto.OrderNumber = input.OrderId;
            //dto.Business = BusinessMapper.BusinessToBusinessDTOV2(input.BusinessOrder.Where(x => x.OrderId == input.OrderId).Select(x => x.Business).FirstOrDefault());
            dto.Products = DBProductMapper.ProductsToListOfProductDTOs(orderProducts.Where(x => x.OrderId == input.OrderId).Select(x => x.Product).ToList());
            foreach (var product in dto.Products)
            {
                product.Amount = orderProducts.Where(x => x.ProductId == product.Id).Select(x => x.Amount).Single();
            }
            return dto;
        }
    }
}

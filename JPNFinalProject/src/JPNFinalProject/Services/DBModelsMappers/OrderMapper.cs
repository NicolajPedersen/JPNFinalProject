using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class OrderMapper
    {
        public static Order OrderDTOToOrder(OrderDTO dto, int personId) {
            return new Order() {
                PersonId = personId,
                TotalPrice = Convert.ToDecimal(dto.TotalPrice), //Da decimal i databasen kun tillader hele tal, så kan man ikke få kommatal med og dermed er prisen ikke korrekt
                DeliveryTime = new DateTimeOffset()
            };
        }

        public static OrderDTO OrderToOrderDTO(Order input, List<Product> products) {
            return new OrderDTO() {
                Person = PersonMapper.PersonToPersonDTO(input.Person),
                CustomerMail = input.Person.Email,
                Products = DBProductMapper.ProductsToListOfProductDTOs(products),
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
            dto.OrderNumber = 22222;
            //dto.Business = BusinessMapper.BusinessToBusinessDTOV2(input.BusinessOrder.Where(x => x.OrderId == input.OrderId).Select(x => x.Business).FirstOrDefault());
            dto.Products = DBProductMapper.ProductsToListOfProductDTOs(orderProducts.Where(x => x.OrderId == input.OrderId).Select(x => x.Product).ToList());
            foreach (var product in dto.Products)
            {
                product.Amount = orderProducts.Where(x => x.ProductId == product.Id).Select(x => x.Amount).Single();
            }
            return dto;
        }

        public static List<OrderProduct> OrderDTOToOrderProduct(int orderId, List<ProductDTO> products) {
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach (var product in products) {
                OrderProduct orderProduct = new OrderProduct();
                orderProduct.Amount = product.Amount;
                orderProduct.OrderId = orderId;
                orderProduct.Price = Convert.ToDecimal(product.Amount * product.Price);
                orderProduct.ProductId = product.Id;
                orderProducts.Add(orderProduct);
            }

            return orderProducts;
        }
    }
}

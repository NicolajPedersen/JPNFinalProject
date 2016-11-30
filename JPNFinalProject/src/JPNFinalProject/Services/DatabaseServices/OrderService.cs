using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPNFinalProject.Data.DatabaseBrokers;
using JPNFinalProject.Services.DBModelsMappers;
using Microsoft.AspNetCore.Http;

namespace JPNFinalProject.Services.DatabaseServices
{
    public class OrderService
    {
        private OrderBroker _broker;
        private SessionContainer _sessionContainer;
        public OrderService() {
            _broker = new OrderBroker();
            _sessionContainer = new SessionContainer();
        }

        public BusinessDTO GetBusinessById(int id) {
            return BusinessMapper.BusinessToBusinessDTO(_broker.GetBusinessById(id));
        }

        public int SaveOrder(HttpContext context) {
            var order = _sessionContainer.GetOrderFromSession(context, "order");
            var personId = _broker.SavePerson(PersonMapper.PersonDTOToPerson(order.Person));

            return _broker.SaveOrder(OrderMapper.OrderDTOToOrder(order, personId));
        }

        public OrderDTO GetOrderByOrderNumber(int orderId) {
            return OrderMapper.OrderToOrderDTO(_broker.GetOrderByOrderNumber(orderId));
        }

        public List<OrderDTO> GetOrders()
        {
            var dbOrders = _broker.GetOrders();

            List<OrderDTO> orderList = new List<OrderDTO>();

            foreach (var order in dbOrders)
            {
                var orderProducts = _broker.GetOrderProducts(order.OrderId);
                orderList.Add(OrderMapper.OrderToOrderDTOV2(order, orderProducts));
            }

            return orderList;

        }
    }
}

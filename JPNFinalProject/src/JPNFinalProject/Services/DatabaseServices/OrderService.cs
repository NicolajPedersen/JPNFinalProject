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
            var businessId = _broker.SaveBusiness(BusinessMapper.BusinessDTOToBusiness(order.Business));

            var orderId = _broker.SaveOrder(OrderMapper.OrderDTOToOrder(order, personId));

            _broker.SaveBusinessOrder(businessId, orderId);

            return orderId;
        }

        public OrderDTO GetOrderByOrderNumber(int orderId) {
            return OrderMapper.OrderToOrderDTO(_broker.GetOrderByOrderNumber(orderId));
        }
    }
}

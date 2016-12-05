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
        private OrderBroker _orderBroker;
        private ProductBroker _productBroker;
        private SessionContainer _sessionContainer;
        public OrderService() {
            _orderBroker = new OrderBroker();
            _productBroker = new ProductBroker();
            _sessionContainer = new SessionContainer();
        }

        public BusinessDTO GetBusinessById(int id) {
            return BusinessMapper.BusinessToBusinessDTO(_orderBroker.GetBusinessById(id));
        }

        public int SaveOrder(HttpContext context) {
            var order = _sessionContainer.GetOrderFromSession(context, "order");
            var personId = _orderBroker.SavePerson(PersonMapper.PersonDTOToPerson(order.Person));
            //var businessId = _broker.SaveBusiness(BusinessMapper.BusinessDTOToBusiness(order.Business));

            var orderId = _orderBroker.SaveOrder(OrderMapper.OrderDTOToOrder(order, personId));

            _orderBroker.SaveBusinessOrder(order.Business.Id, orderId);
            _orderBroker.SaveOrderProduct(OrderMapper.OrderDTOToOrderProduct(orderId, order.Products));

            return orderId;
        }

        public OrderDTO GetOrderByOrderNumber(int orderId) {
            var o = _orderBroker.GetOrderByOrderNumber(orderId);
            var ops = o.OrderProduct.Where(x => x.OrderId == orderId).Select(x => x).ToList();
            var products = _productBroker.GetProductsById(ops);
            var order = OrderMapper.OrderToOrderDTO(o, products);
            order.Business = BusinessMapper.BusinessToBusinessDTO(_orderBroker.GetBusinessById(o.BusinessOrder.Where(x => x.OrderId == o.OrderId).Select(x => x.BusinessId).Single()));
            return order;
        }

        public List<OrderDTO> GetOrders()
        {
            var dbOrders = _orderBroker.GetOrders();

            List<OrderDTO> orderList = new List<OrderDTO>();

            foreach (var order in dbOrders)
            {
                var orderProducts = _orderBroker.GetOrderProducts(order.OrderId);
                orderList.Add(OrderMapper.OrderToOrderDTOV2(order, orderProducts));
            }

            return orderList;

        }
    }
}

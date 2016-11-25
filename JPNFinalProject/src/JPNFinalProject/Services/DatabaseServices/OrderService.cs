using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPNFinalProject.Data.DatabaseBrokers;
using JPNFinalProject.Services.DBModelsMappers;

namespace JPNFinalProject.Services.DatabaseServices
{
    public class OrderService
    {
        private OrderBroker _broker;
        public OrderService(OrderBroker broker) {
            _broker = broker;
        }

        public BusinessDTO GetBusinessById(int id) {
            return BusinessMapper.BusinessToBusinessDTO(_broker.GetBusinessById(id));
        }
    }
}

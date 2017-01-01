using JPNFinalProject.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JPNFinalProject.Data.DatabaseBrokers
{
    public class OrderBroker
    {
        public virtual Business GetBusinessById(int id) {
            using (var context = new JPNFinalProjectContext()) {
                return context.Business.Where(x => x.BusinessId == id).Select(x => x).Include(x => x.Address).First();
            }
        }

        public virtual int SavePerson(Person person) {
            using (var context = new JPNFinalProjectContext()) {
                context.Person.Add(person);
                context.SaveChanges();

                return context.Person.Last().PersonId;
            }
        }

        public virtual int SaveBusiness(Business business) {
            using (var context = new JPNFinalProjectContext()) {
                context.Business.Add(business);
                context.SaveChanges();

                return context.Business.Last().BusinessId;
            }
        }

        public virtual int SaveOrder(Order order) {
            using (var context = new JPNFinalProjectContext()) {
                context.Order.Add(order);
                context.SaveChanges();
                return context.Order.Last().OrderId;
            }
        }

        public virtual void SaveBusinessOrder(int businessId, int orderId) {
            using (var context = new JPNFinalProjectContext()) {
                BusinessOrder entity = new BusinessOrder() { BusinessId = businessId, OrderId = orderId };
                context.BusinessOrder.Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void SaveOrderProduct(List<OrderProduct> orderProducts) {
            using (var context = new JPNFinalProjectContext()) {
                foreach (var op in orderProducts) {
                    context.OrderProduct.Add(op);
                }
                context.SaveChanges();
            }
        }

        public virtual Order GetOrderByOrderNumber(int orderId) {
            using (var context = new JPNFinalProjectContext()) {
                return context.Order.Include(x => x.Person).Include(x => x.Person.Address).Include(x => x.BusinessOrder).Include(x => x.OrderProduct).Where(x => x.OrderId == orderId).Select(x => x).Single();
            }
        }

        public virtual Business GetBusinessOrder(int orderId)
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.BusinessOrder.Where(x => x.OrderId == orderId)
                    .Select(x => x.Business)
                    .Single();
            }
        }

        public virtual List<Order> GetOrders()
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.Order
                    .Include(x => x.Person)
                    .Include(x => x.Person.Address)
                    .Include(x => x.BusinessOrder)
                    .Include(x => x.OrderProduct)
                    .ToList();
            }
        }

        public virtual List<OrderProduct> GetOrderProducts(int orderId)
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.OrderProduct.Where(x => x.OrderId == orderId).Select(x => x)
                    .Include(x => x.Product)
                    .ToList();
            }
        }
        public virtual List<Business> GetBusinesses()
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.Business.Include(x => x.Address).ToList();
            }
        }
        public virtual List<Business> GetBusinessesByPostal(string postalCode)
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.Business
                    .Where(x => x.Address.PostalCode == postalCode)
                    .Include(x => x.Address)
                    .ToList();
            }
        }

        public virtual Person GetPersonByOrderId(int orderId) {
            using (var context = new JPNFinalProjectContext()) {
                var personId = context.Order.Where(x => x.OrderId == orderId).Select(x => x.PersonId).Single();

                return context.Person.Where(x => x.PersonId == personId).Include(x => x.Address).Select(x => x).Single();
            }
        }

        public virtual void DeleteOrder(int orderId) {
            using (var context = new JPNFinalProjectContext()) {
                var businessOrder = context.BusinessOrder.Where(x => x.OrderId == orderId).Select(x => x).Single();
                context.BusinessOrder.Remove(businessOrder);

                var orderProducts = context.OrderProduct.Where(x => x.OrderId == orderId).Select(x => x).ToList();
                orderProducts.ForEach(x => context.OrderProduct.Remove(x));

                var order = context.Order.Where(x => x.OrderId == orderId).Select(x => x).Single();

                var person = context.Person.Where(x => x.PersonId == order.PersonId).Select(x => x).Single();
                context.Person.Remove(person);

                context.Order.Remove(order);

                context.SaveChanges();
            }
        }
    }
}

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
                return context.Business.Where(x => x.BusinessId == id).Select(x => x).First();
            }
        }

        public virtual int SavePerson(Person person) {
            using (var context = new JPNFinalProjectContext()) {
                context.Person.Add(person);
                context.SaveChanges();

                return context.Person.Last().PersonId;
            }
        }

        public virtual int SaveOrder(Order order) {
            using (var context = new JPNFinalProjectContext()) {
                context.Order.Add(order);
                context.SaveChanges();
                return context.Order.Last().OrderId;
            }
        }

        public virtual Order GetOrderByOrderNumber(int orderId) {
            using (var context = new JPNFinalProjectContext()) {
                return context.Order.Include(x => x.Person).Include(x => x.BusinessOrder).Include(x => x.OrderProduct).Where(x => x.OrderId == orderId).Select(x => x).Single();
            }
        }
    }
}

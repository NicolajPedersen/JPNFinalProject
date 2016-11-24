using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Order
    {
        public Order()
        {
            BusinessOrder = new HashSet<BusinessOrder>();
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset DeliveryTime { get; set; }

        public virtual ICollection<BusinessOrder> BusinessOrder { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual Person Person { get; set; }
    }
}

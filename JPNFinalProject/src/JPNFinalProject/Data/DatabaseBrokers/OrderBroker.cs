using JPNFinalProject.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DatabaseBrokers
{
    public class OrderBroker
    {
        public virtual Business GetBusinessById(int id) {
            using (var context = new JPNFinalProjectContext()) {
                return context.Business.Where(x => x.BusinessId == id).Select(x => x).First();
            }
        }
    }
}

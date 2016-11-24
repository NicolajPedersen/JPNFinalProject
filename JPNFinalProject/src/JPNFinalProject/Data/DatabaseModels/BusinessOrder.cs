using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class BusinessOrder
    {
        public int BusinessOrderId { get; set; }
        public int BusinessId { get; set; }
        public int OrderId { get; set; }

        public virtual Business Business { get; set; }
        public virtual Order Order { get; set; }
    }
}

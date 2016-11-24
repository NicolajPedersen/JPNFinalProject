using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class BusinessProduct
    {
        public int BusinessProductId { get; set; }
        public int BusinessId { get; set; }
        public int ProductId { get; set; }

        public virtual Business Business { get; set; }
        public virtual Product Product { get; set; }
    }
}

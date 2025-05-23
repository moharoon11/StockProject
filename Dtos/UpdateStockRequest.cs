using api.Models;
using System.Collections.Generic;

namespace api.Dtos {

    public class UpdateStockRequest {

         public string Symbol {get; set; }

         public string CompanyName {get; set; }

         public decimal Purchase {get; set; }

         public decimal LastDiv { get; set; }

         public string Industry { get; set; }

         public long MarketCap { get; set; }

            public List<Comment>? Comments { get; set; }
    }
}
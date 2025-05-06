using api.Models;

namespace api.Dtos {

    public class CreateStockRequest {

         public string Symbol {get; set; }

         public string CompanyName {get; set; }

         public decimal Purchase {get; set; }

         public decimal LastDiv { get; set; }

         public string Industry { get; set; }

         public long MarketCap { get; set; }

         public List<Comment>? Comments { get; set; }
    }
}
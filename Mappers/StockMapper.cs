using api.Models;
using api.Dtos;

namespace api.Mappers {

   public static class StockMapper {

       
       public static StockDto toStockDto(this Stock stockModel) {
           return new StockDto {
              Id = stockModel.Id,
              Symbol = stockModel.Symbol,
              CompanyName = stockModel.CompanyName,
              Purchase = stockModel.Purchase,
              LastDiv = stockModel.LastDiv,
              Industry = stockModel.Industry,
              MarketCap = stockModel.MarketCap
           };
       }

       public static Stock toStockFromCSR(this CreateStockRequest stockRequest) {
          return new Stock {
               Symbol = stockRequest.Symbol,
              CompanyName = stockRequest.CompanyName,
              Purchase = stockRequest.Purchase,
              LastDiv = stockRequest.LastDiv,
              Industry = stockRequest.Industry,
              MarketCap = stockRequest.MarketCap
          };
       }
   }

}
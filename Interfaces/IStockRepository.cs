using api.Dtos;
using api.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task UpdateStockAsync(Stock stock);
        Task DeleteStockAsync(int id);
    }
}
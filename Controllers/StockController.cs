using api.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using api.Data;
using api.Dtos;
using api.Mappers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace api.Controllers
{

    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        

        public StockController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<StockDto>>> GetAllStocksAsync() {
            var stocks = await _context.Stocks.ToListAsync();
             
             var stockDtos = stocks.Select(s => s.toStockDto()).ToList();

            if(stockDtos == null) return NotFound("No stocks available");

            return Ok(stockDtos);
        }


         [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetByIdAsync([FromRoute] int id) {
            var stock = await _context.Stocks.FindAsync(id);

            if(stock == null) {
                return NotFound("Stock not found");
            }

            return Ok(stock.toStockDto());
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStockAsync([FromBody] CreateStockRequest createStockRequest) {
             Stock stock = createStockRequest.toStockFromCSR();

             await _context.Stocks.AddAsync(stock);
             await _context.SaveChangesAsync();

             return CreatedAtAction(nameof(GetByIdAsync), new {Id = stock.Id}, stock.toStockDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StockDto>> UpdateStockAsync([FromRoute] int id, [FromBody] UpdateStockRequest updateStockRequest) {

            Stock stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

                  
            if(stock == null) return NotFound("Stock Not Found");

            stock.Symbol = updateStockRequest.Symbol;
            stock.CompanyName = updateStockRequest.CompanyName;
            stock.Purchase = updateStockRequest.Purchase;
            stock.LastDiv = updateStockRequest.LastDiv;
            stock.Industry = updateStockRequest.Industry;
            stock.MarketCap = updateStockRequest.MarketCap;
            
            
            await _context.SaveChangesAsync();

            return Ok(stock.toStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStockAsync([FromRoute] int id) {

            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(stock == null) return NotFound("Stock Not Found");

            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();
            return Ok("Stock Deleted");
        }
    }
}


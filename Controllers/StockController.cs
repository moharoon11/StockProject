using api.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using api.Data;
using api.Dtos;
using api.Mappers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{

    [ApiController]
    [Route("api/stock")]
    public class StockController(IStockRepository stockRepo) : ControllerBase
    {

      

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<StockDto>>> GetAllStocksAsync() {
            var stocks = await stockRepo.GetAllStocksAsync();


            if (stocks == null || stocks.Count == 0)
            {
                return NotFound("No stocks found.");
            }

            var allstocks =stocks.Select(stock => stock.toStockDto()).ToList();
            return Ok(allstocks);
        }


         [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetByIdAsync([FromRoute] int id) {
            var stock = await stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound($"Stock with id {id} not found.");
            }

            var dto = stock.toStockDto();
            return Ok(dto);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<StockDto>> CreateStockAsync([FromBody] CreateStockRequest createStockRequest) {
            if (createStockRequest == null)
            {
                return BadRequest("Invalid stock data.");
            }

              var stock = createStockRequest.toStockFromCSR();

            var createdStock = await stockRepo.CreateStockAsync(stock);
            return Ok(createdStock.toStockDto());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStockAsync([FromRoute] int id, [FromBody] UpdateStockRequest updateStockRequest) {

           var stock = await stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound($"Stock with id {id} not found.");
            }

            if (updateStockRequest == null)
            {
                return BadRequest("Invalid stock data.");
            }

            stock.Symbol = updateStockRequest.Symbol;
            stock.CompanyName = updateStockRequest.CompanyName;
            stock.Purchase = updateStockRequest.Purchase;
            stock.LastDiv = updateStockRequest.LastDiv;
            stock.Industry = updateStockRequest.Industry;
            stock.MarketCap = updateStockRequest.MarketCap;

        

            await stockRepo.UpdateStockAsync(stock);
            return NoContent();
            
        
        }
         
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStockAsync([FromRoute] int id) {
            var existingStock = await stockRepo.GetByIdAsync(id);
            if (existingStock == null)
            {
                return NotFound($"Stock with id {id} not found.");
            }

            await stockRepo.DeleteStockAsync(id);
            return NoContent();
           
        }
    }
}


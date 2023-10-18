using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminar.Data;
using seminar.Policy;
using seminar.Repositories;

namespace seminar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenController : ControllerBase
    {
        private readonly ICrudRepository _productRepo;

        public PenController([FromKeyedServices("pen")] ICrudRepository repo) {
            _productRepo = repo;
        }

        // GET: /api/Products
        
        [HttpGet]
        public async Task<IActionResult> GetAllPens() {
            try
            {
                return Ok(await _productRepo.GetAllAsync());
            }
            catch (Exception ex) { return BadRequest(); }    
        }

        // GET: /api/Products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPenById(int id)
        {
            var book = await _productRepo.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }
        // POST: /api/Products
        [HttpPost]
        public async Task<IActionResult> AddPen(Pen book)
        {
            try
            {
                var newBook = await _productRepo.CreateAsync(book);
                return Ok(newBook);
            }
            catch
            {
                return BadRequest();
            }

        }
        // PUT: /api/Products/id
        [HttpPut("{id}")]
        [MinimumAgeAuthorize(16)]
        [Authorize]
        public async Task<IActionResult> UpdatePen(int id, Pen book)
        {
            try
            {
                var newBook = await _productRepo.UpdateAsync(id, book);
                return Ok(newBook);
            }
            catch
            {
                return BadRequest();
            }
        }
        // DELETE 
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePen(int id)
        {
            try
            {
                var book = await _productRepo.DeleteAsync(id);
                if (book == null) return NotFound();
                return Ok(book);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}

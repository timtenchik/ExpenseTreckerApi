using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTreckerApi.Models;
using ExpenseTreckerApi.Data;

namespace ExpenseTreckerApi.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class IcomeController : ControllerBase
	{
        private readonly IncomeTreckerApiContext _incomeContext;

        public IcomeController(IncomeTreckerApiContext income_context)
		{
            _incomeContext = income_context;

        }

        // GET: api/Income
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incomes>>> GetIncome()
        {
            return await _incomeContext.Income.Include("IncomeCategory").ToListAsync();
        }

        // GET: api/Income/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Incomes>> GetIncome(int id)
        {
            var income = await _incomeContext.Income.Include("IncomeCategory").FirstOrDefaultAsync(x => x.Id == id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Income/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, Incomes incomes)
        {
            if (id != incomes.Id)
            {
                return BadRequest();
            }

            _incomeContext.Entry(incomes).State = EntityState.Modified;

            try
            {
                await _incomeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Income
        [HttpPost]
        public async Task<ActionResult<Incomes>> PostIncome(Incomes income)
        {
            income.IncomesCategory = await _incomeContext.IncomeCategory.Where(x => x.Id == income.IncomesCategoryId).FirstOrDefaultAsync();
            _incomeContext.Income.Add(income);
            await _incomeContext.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.Id }, income);
        }

        // DELETE: api/Income/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _incomeContext.Income.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _incomeContext.Income.Remove(income);
            await _incomeContext.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return _incomeContext.Income.Any(e => e.Id == id);
        }
    }
}

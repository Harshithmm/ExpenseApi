using ExpensesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Entry>>> GetAllEntries()
        {
            return Ok(await context.Entries.ToListAsync());
        }

        [HttpGet("{id}")]
        public ActionResult<Entry> GetEntryById(int id)
        {
           var entriesById= context.Entries.Find(id);
           if (entriesById == null)
               return NotFound();
           return Ok(entriesById);
        }

        [HttpPost]
        public async Task<ActionResult<List<Entry>>> AddEntry([FromBody] Entry addEntry)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await context.Entries.AddAsync(addEntry);
                await context.SaveChangesAsync();
                return Ok(addEntry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEntry([FromRoute] int id, [FromBody] Entry editEntry)
        {
            var entryToUpdate =await context.Entries.FindAsync(id);
            if (entryToUpdate == null)
            {
                return NotFound();
            }

            entryToUpdate.Description = editEntry.Description;
            entryToUpdate.IsExpense = editEntry.IsExpense;
            entryToUpdate.Value = editEntry.Value;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEntry([FromRoute] int id)
        {
            var entryToDelete= context.Entries.Find(id);
            if (entryToDelete == null)
            {
                return NotFound();
            }

            context.Entries.Remove(entryToDelete);
            context.SaveChanges();
            return Ok();
        }
    }
}

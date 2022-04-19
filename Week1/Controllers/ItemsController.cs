using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Week1.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Week1.Data;

namespace Week1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MasterDataContext dbContext;

        public ItemsController(MasterDataContext context)
        {
            dbContext = context;

            if (dbContext.Items.Count() == 0)
            {
                // Create a new Item if collection is empty,
                // which means you can't delete all Items.
                dbContext.Items.Add(new Item { Id = 1, Name = "Item1", VendorValue = 150 });
                dbContext.Items.Add(new Item { Id = 2, Name = "Item2", VendorValue = 150 });
                dbContext.Items.Add(new Item { Id = 3, Name = "Item3", VendorValue = 150 });
                dbContext.SaveChanges();
            }
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return dbContext.Items.ToList();
        }



        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var Item = dbContext.Items.FirstOrDefault(x => x.Id == id);

            if (Item == null)
            {
                return NotFound();
            }

            return Item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            dbContext.Entry(item).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            var item = dbContext.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            dbContext.Items.Remove(item);
            dbContext.SaveChanges();

            return NoContent();
        }

        private bool ItemExists(long id)
        {
            return dbContext.Items.Any(e => e.Id == id);
        }


    }
}
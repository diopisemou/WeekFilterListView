using System;
using Microsoft.AspNetCore.Mvc;

using CustomListView.Models;
using CustomListView.Models.ItemTodo;

namespace CustomListView.Controllers
{
    [Route("api/[controller]")]
    public class ItemTodoController : Controller
    {

        private readonly IItemTodoRepository ItemRepository;

        public ItemController(IItemTodoRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ItemTodo GetItem(string id)
        {
            ItemTodo item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]ItemTodo item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ItemRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] ItemTodo item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ItemRepository.Remove(id);
        }
    }
}

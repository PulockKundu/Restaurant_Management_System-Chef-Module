using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(CategoriesRepo repo) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = repo.GetAll();
            return Ok(result);
        }


        [HttpGet("GetByID/{id}")]
        public IActionResult GetById(int id)
        {
            var result = repo.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Save(Categories model)
        {
            var result = repo.Save(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = repo.Delete(id);
            return Ok(result);
        }


    }
}

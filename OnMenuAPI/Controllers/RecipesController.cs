using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OnMenuAPI.Controllers
{
    /// <summary>
    /// Controller for a REST api returning recipes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        /// <summary>
        /// Gets the recipes
        /// </summary>
        /// <returns>The recipes as a Json array</returns>
        // GET: api/Recipes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets a recipe by it's id
        /// </summary>
        /// <param name="id">The recipe id</param>
        /// <returns>The recipe, as a Json</returns>
        // GET: api/Recipes/5
        [HttpGet("{id}", Name = "GetRecipes")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Posts a recipe to the server
        /// </summary>
        /// <param name="value">A Json containing the recipe</param>
        // POST: api/Recipes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Puts a recipe in a route corresponding to an id
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="value">A Json containint the recipe</param>
        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes a recipe from the server
        /// </summary>
        /// <param name="id">The id from the ingredient</param>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

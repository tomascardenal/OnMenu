using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Mvc;
using OnMenuAPI.Data;
using OnMenuAPI.Models;

namespace OnMenuAPI.Controllers
{
    /// <summary>
    /// Controller for a REST api returning ingredients
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {


        /// <summary>
        /// Gets the ingredients
        /// </summary>
        /// <returns>The ingredients as a Json array</returns>
        // GET: api/Ingredients
        [HttpGet]
        public IEnumerable<string> Get()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Ingredient));
            List<string> jsonArray = new List<string>();
            foreach (Ingredient i in DataContainer.Ingredients)
            {
                MemoryStream stream1 = new MemoryStream();
                ser.WriteObject(stream1, i);
                stream1.Position = 0;
                using (StreamReader sr = new StreamReader(stream1))
                {
                    jsonArray.Add(sr.ReadToEnd());
                }
            }
            return jsonArray;
        }
        /// <summary>
        /// Gets an ingredient by it's id
        /// </summary>
        /// <param name="id">The ingredient id</param>
        /// <returns>The ingredient, as a Json</returns>
        // GET: api/Ingredients/5
        [HttpGet("{id}", Name = "GetIngredients")]
        public string Get(int id)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Ingredient));
            string jsonObject = "";
            Ingredient i = DataContainer.Ingredients[id];
            ser.WriteObject(stream1, i);
            stream1.Position = 0;
            using (StreamReader sr = new StreamReader(stream1))
            {
                jsonObject = sr.ReadToEnd();
            }
            return jsonObject;
        }

        /// <summary>
        /// Posts an ingredient to the server
        /// </summary>
        /// <param name="value">A Json containing the ingredient</param>
        // POST: api/Ingredients
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Puts an ingredient in a route corresponding to an id
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="value">A Json containint the ingredient</param>
        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes an ingredient from the server
        /// </summary>
        /// <param name="id">The id from the ingredient</param>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

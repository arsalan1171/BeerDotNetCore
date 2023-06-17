using Microsoft.AspNetCore.Mvc;
using BeerDotNetCore.Models;

namespace BeerDotNetCore.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly BeerContext _context;
        private readonly HttpClient _client;

        public BeerController(BeerContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: api/Beer
        /// <summary>
        /// Gets a list of beers
        /// </summary>
        /// <response code="200">Returns the list of beer</response>
        /// <response code="404">the requested list is null</response>
        [HttpGet("menu")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers()
        {
            string apiUrl = "https://api.punkapi.com/v2/beers";
            List<Beer>? beerList;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                beerList = await response.Content.ReadFromJsonAsync<List<Beer>>();

                if (beerList == null)
                {
                    return NotFound();
                }

                return Ok(beerList);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        // GET: api/Beer/5
        /// <summary>
        /// Gets beer by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the requested beer</response>
        /// <response code="404"> the item is null</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            string apiUrl = "https://api.punkapi.com/v2/beers/" + id;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var res = await response.Content.ReadAsStringAsync();

                List<Beer>? beerList = await response.Content.ReadFromJsonAsync<List<Beer>>();
                Beer? beer = beerList?.FirstOrDefault();

                if (beer == null)
                {
                    return NotFound();
                }

                return Ok(beer);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        // GET: api/Beer/random
        /// <summary>
        /// Gets a random beer
        /// </summary>
        /// <response code="200">Returns a random beer</response>
        /// <response code="404"> the requested beer is null</response>
        [HttpGet("random")]
        public async Task<ActionResult<Beer>> GetRandomBeer()
        {
            string apiUrl = "https://api.punkapi.com/v2/beers/random";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var res = await response.Content.ReadAsStringAsync();

                List<Beer>? beerList = await response.Content.ReadFromJsonAsync<List<Beer>>();
                Beer? beer = beerList?.FirstOrDefault();

                if (beer == null)
                {
                    return NotFound();
                }

                return Ok(beer);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Searches for beer by query
        /// </summary>
        /// <param name="search_query"></param>
        [HttpGet("/search/{search_query}")]
        public async Task<ActionResult<Beer>> SearchBeers(string search_query)
        {
            string apiUrl = "https://api.punkapi.com/v2/beers?beer_name=" + search_query;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var res = await response.Content.ReadAsStringAsync();

                List<Beer>? beerList = await response.Content.ReadFromJsonAsync<List<Beer>>();

                if (beerList == null)
                {
                    return NotFound();
                }

                return Ok(beerList);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}

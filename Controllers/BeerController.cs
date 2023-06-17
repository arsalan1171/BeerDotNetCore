using Microsoft.AspNetCore.Mvc;
using BeerDotNetCore.Models;

namespace BeerDotNetCore.Controllers
{
    [Route("[controller]")]
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

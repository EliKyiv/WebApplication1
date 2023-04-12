using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        // GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            List<Car> cars = new List<Car>
    {
        new Car { Maker = "Toyota", Model = "Camry", Year = 2022, Body = "Sedan", Engine="V6" },
        new Car { Maker = "Honda", Model = "Civic", Year = 2021, Body = "Coupe", Engine="2.0" },
        new Car { Maker = "Ford", Model = "Mustang", Year = 2020, Body = "Convertible", Engine="V8" }
    };
            return cars;
        }
        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private readonly IManagementCars _managementCars;

        public CarsController(IManagementCars managementCars)
        {
            _managementCars = managementCars;
        }

    }
    public class Car : IManagementCars
    {
        public string Maker { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Body { get; set; }
        public string Engine { get; set; }

        public async Task<string> GetCarName()
        {
            return await Task.FromResult(Maker + " " + Model);
        }

        public async Task<string> GetCarEngine()
        {
            return await Task.FromResult(Engine);
        }

        public async Task<int> GetCarAge()
        {
            return await Task.FromResult(DateTime.Now.Year - Year);
        }
    }
    public interface IManagementCars
    {
        Task<string> GetCarName();
        Task<string> GetCarEngine();
        Task<int> GetCarAge();
    }
}

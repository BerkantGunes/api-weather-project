using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project6_ApiWeather.Context;
using Project6_ApiWeather.Entities;

namespace Project6_ApiWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
        WeatherContext context = new WeatherContext();

        [HttpGet]
        public IActionResult WeatherCityList() 
        {
            var values = context.Cities.ToList(); // Sehir Listesini bana getir.
            return Ok(values); // valuesi Swaggerda ki Ok metotu içinde döndürürüz.
        }
        // Ekleme işlemi yapmak için;
        [HttpPost]

        public IActionResult CreateWeatherCity(City city) // Parametre olarak City sınıfından bir city verdik.
        {
            context.Cities.Add(city);
            context.SaveChanges();
            return Ok("City Added Successfully!");
        }

        [HttpDelete]
        public IActionResult DeleteWeatherCity(int id)
        {
            var value = context.Cities.Find(id);
            context.Cities.Remove(value);
            context.SaveChanges();
            return Ok("City Deleted Successfully!");
        }

        [HttpPut]
        public IActionResult UpdateWeatherCity(City city) 
        {
            var value = context.Cities.Find(city.CityId); // City i Id ye göre bulduk ve degiskene atadik.
            value.CityName = city.CityName;
            value.Detail = city.Detail;
            value.Temp = city.Temp;
            value.Country = city.Country;
            context.SaveChanges();
            return Ok("City Updated Successfully!");
        }

        [HttpGet("GetByIdWeatherCity")]
        public IActionResult GetByIdWeatherCity(int id)
        {
            var value = context.Cities.Find(id);
            return Ok(value);
        }

        [HttpGet("TotalCityCount")]
        public IActionResult TotalCityCount()
        {
            var value = context.Cities.Count();
            return Ok(value);
        }

        [HttpGet("MaxTempCity")]
        public IActionResult MaxTempCity()
        {
            var value = context.Cities.OrderByDescending(x => x.Temp).Select(y => y.CityName).FirstOrDefault();
            return Ok(value);
        }

        [HttpGet("MinTempCity")]
        public IActionResult MinTempCity() // Ankara sonucu dondu.
        {
            var value = context.Cities.OrderBy(x => x.Temp).Select(y => y.CityName).FirstOrDefault();
            return Ok(value);
        }
    }
}

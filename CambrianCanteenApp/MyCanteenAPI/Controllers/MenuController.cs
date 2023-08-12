using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCanteenAPI.Models;
using CanteenApp.Common.Lib;
using Newtonsoft.Json;

namespace MyCanteenAPI.Controllers
{
    [Route("api/Menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MyCanteenDbContext _dbContext;

        public MenuController(ILogger<AccountController> logger, MyCanteenDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        [Route("byId")]
        public IActionResult GetbyID(int Id)
        {
            if (Id > 0)
            {
                Result resultCall = new Result();
                var Item = _dbContext.FoodItems.Where(f => f.Id == Id).FirstOrDefault();
                if (Item != null)
                {
                    resultCall.IsSuccess = true;
                    resultCall.Data = Item;
                }
                else
                {
                    string message = "Item not found.";
                    _logger.LogInformation(message);
                    resultCall.Message = message;
                    return NotFound(message);
                }

                string json = JsonConvert.SerializeObject(resultCall);//, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                Response.ContentType = "application/json";
                _logger.LogInformation("Item by Id fetch request received at " + DateTime.Now.ToShortTimeString(), json);
                return Ok(json);
            }
            else
                return NotFound();
        }

        public IActionResult Get()
        {
            Result resultCall = new Result();
            var menuItems = _dbContext.FoodCategories.Include(f => f.FoodItems).ToList();
            if(menuItems != null)
            {
                resultCall.IsSuccess = true;
                resultCall.Data = menuItems;
            }
            else
            {
                string message = "Menu Items not found.";
                _logger.LogInformation(message);
                resultCall.Message = message;
                return NotFound(message);
            }

            string json = JsonConvert.SerializeObject(resultCall,Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            Response.ContentType = "application/json";
            _logger.LogInformation("Menu Items fetch request received at " + DateTime.Now.ToShortTimeString(), json);
            return Ok(json);

        }
    }
}

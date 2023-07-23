using Microsoft.AspNetCore.Mvc;
using MyCanteenAPI.Models;
using MyCanteenAPI.ViewModels;
using Newtonsoft.Json;

namespace MyCanteenAPI.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MyCanteenDbContext _dbContext;

        public CartController(ILogger<AccountController> logger, MyCanteenDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost]
        [Route("AddCart")]
        public IActionResult AddToCart(Cart cart)
        {
            Result result = new Result();
            if (cart.Studentid > 0 && cart.FoodItemId > 0)
            {
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                result.Message = string.Format("Food Item = {0} and Student ID = {1} Added to the cart.",cart.FoodItemId, cart.Studentid);
                _logger.LogInformation(result.Message);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Incorrect input params for AddToCart. StudentId = {0} and FoodItem = {1}",cart.Studentid,cart.FoodItemId);
                _logger.LogCritical(result.Message);
            }

            string json = JsonConvert.SerializeObject(result);
            Request.ContentType = "application/json";
            return Ok(json);

        }

        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(Cart cart)
        {
            Result result = new Result();
            if (cart.Studentid > 0 && cart.FoodItemId > 0)
            {
                _dbContext.Carts.Remove(cart);
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                result.Message = string.Format("Food Item = {0} and Student ID = {1} deleted from the cart.", cart.FoodItemId, cart.Studentid);
                _logger.LogInformation(result.Message);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Incorrect input params for Delete Cart. StudentId = {0} and FoodItem = {1}", cart.Studentid > 0 , cart.FoodItemId);
                _logger.LogCritical(result.Message);
            }

            string json = JsonConvert.SerializeObject(result);
            Request.ContentType = "application/json";
            return Ok(json);
        }
    }
}

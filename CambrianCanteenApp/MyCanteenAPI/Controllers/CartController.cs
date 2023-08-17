using Microsoft.AspNetCore.Mvc;
using MyCanteenAPI.Models;
using CanteenApp.Common.Lib;
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

        [Obsolete]
        [HttpPost]
        [Route("AddCart")]
        public IActionResult AddCart(Cart cart)
        {
            Result result = new Result();
            if (cart.Studentid > 0 && cart.FoodItemId > 0)
            {
                var existingCart = _dbContext.Carts.Where(c => c.FoodItemId == cart.FoodItemId && c.Studentid == cart.Studentid).FirstOrDefault();
                if (existingCart != null)
                {
                    existingCart.Qty += 1;
                    _dbContext.Update(existingCart);
                    result.Message = string.Format("Quantity is incremented to the cart Id.{0}", cart.Id);
                }
                else
                {
                    Cart _cart = new Cart();
                    _dbContext.Carts.Add(_cart);
                    result.Message = string.Format("Food Item = {0} and Student ID = {1} Added to the cart.", cart.FoodItemId, cart.Studentid);
                }
                _dbContext.SaveChanges();
                result.IsSuccess = true;
               
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

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(InputCart inputCart)
        {
            Result result = new Result();
            if (inputCart.studentId > 0 && inputCart.foodItemId > 0)
            {
                var cart = _dbContext.Carts.Where<Cart>(c => c.FoodItemId == inputCart.foodItemId && c.Studentid == inputCart.studentId).FirstOrDefault();
                if (cart != null)
                {
                    // var existingCart = _dbContext.Carts.Where(c => c.FoodItemId == cart.FoodItemId && c.Studentid == cart.Studentid).FirstOrDefault();

                    cart.Qty += 1;
                    var output =_dbContext.Update(cart);
                    result.Message = string.Format("Quantity is incremented to the cart Id.{0}", cart.Id);
                }
                else
                {
                    Cart newCart = new Cart();
                    newCart.FoodItemId = inputCart.foodItemId;
                    newCart.Studentid = inputCart.studentId;
                    newCart.Qty = 1;
                    newCart.AddedOn = DateTime.Now;
                    _dbContext.Carts.Add(newCart);
                    result.Message = string.Format("Food Item = {0} and Student ID = {1} Added to the cart.", newCart.FoodItemId, newCart.Studentid);
                }
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                _logger.LogInformation(result.Message);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Incorrect input params for AddToCart. StudentId = {0} and FoodItem = {1}", inputCart.studentId, inputCart.foodItemId);
                _logger.LogCritical(result.Message);
            }

            //string json = JsonConvert.SerializeObject(result);
            var updatedCart = (from c in _dbContext.Carts join f in _dbContext.FoodItems on c.FoodItemId equals f.Id
                               orderby c.Id ascending
                               select new
                               {
                                   StudentId = c.Studentid,
                                   FoodItemId = c.FoodItemId,
                                   FoodItemName = f.ItemName,
                                   Price = f.Price,
                                   ImageURL = f.ImageUrl,
                                   QTY = c.Qty
                               }).ToList();

            List<CartVM> cartVM = new List<CartVM>();
            foreach (var item in updatedCart)
            {
                CartVM cart = new CartVM();
                cart.studentId = item.StudentId;
                cart.price = item.Price.ToString();
                cart.imageURL = item.ImageURL;
                cart.foodItemName = item.FoodItemName;
                cart.foodItemId = item.FoodItemId;
                cart.qty = item.QTY;

                cartVM.Add(cart);
            }
            
            Request.ContentType = "application/json";
            result.Data = cartVM;
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(InputCart inputCart)
        {
            Result result = new Result();
            if (inputCart.studentId > 0 && inputCart.foodItemId > 0)
            {
                if (inputCart.fullEmptyCart)
                {
                    var removeCart = _dbContext.Carts.Where<Cart>(c => c.FoodItemId == inputCart.foodItemId && c.Studentid == inputCart.studentId).FirstOrDefault();
                    if (removeCart != null)
                    {
                        _dbContext.Carts.Remove(removeCart);
                        result.Message = string.Format("The cart with Id {0} is removed.", removeCart.Id);
                    }
                }
                else
                {
                    var cart = _dbContext.Carts.Where<Cart>(c => c.FoodItemId == inputCart.foodItemId && c.Studentid == inputCart.studentId && c.Qty > 1).FirstOrDefault();
                    if (cart != null)
                    {
                        // var existingCart = _dbContext.Carts.Where(c => c.FoodItemId == cart.FoodItemId && c.Studentid == cart.Studentid).FirstOrDefault();
                        cart.Qty -= 1;
                        _dbContext.Update(cart);
                        result.Message = string.Format("Quantity is decremented to the cart Id.{0}", cart.Id);
                    }
                    else
                    {
                        var deleteCart = _dbContext.Carts.Where<Cart>(c => c.FoodItemId == inputCart.foodItemId && c.Studentid == inputCart.studentId).FirstOrDefault();
                        if (deleteCart != null)
                        {
                            _dbContext.Carts.Remove(deleteCart);
                            result.Message = string.Format("The cart with Id {0} is removed.", deleteCart.Id);
                        }
                        else
                            result.Message = string.Format("The cart not found.");
                    }
                }
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                _logger.LogInformation(result.Message);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Incorrect input params for delete cart. StudentId = {0} and FoodItem = {1}", inputCart.studentId, inputCart.foodItemId);
                _logger.LogCritical(result.Message);
            }

            //string json = JsonConvert.SerializeObject(result);
            Request.ContentType = "application/json";
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCanteenAPI.Models;
using CanteenApp.Common.Lib;
using Newtonsoft.Json;

namespace MyCanteenAPI.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MyCanteenDbContext _dbContext;

        public OrderController(ILogger<AccountController> logger, MyCanteenDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(InputOrder inputOrder)
        {
            Result result = new Result();
            if (inputOrder.foodItems.Count > 0 && inputOrder.studentId > 0)
            {
                Order newOrder = new Order();
                newOrder.StudentId = inputOrder.studentId;
                newOrder.OrderNumber = string.Format("CAM-ORD-{0}{1}{2}{3}{4}{5}{6}", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                newOrder.Status = new OrderStatus { Name = "Confirmed" };
                newOrder.CreatedOn = DateTime.Now;
                newOrder.Total = inputOrder.total;
                newOrder.Tax = inputOrder.tax;
                foreach(int itemId in inputOrder.foodItems)
                {
                 //   var _foodItem = _dbContext.FoodItems.Where<FoodItem>(f => f.Id == itemId).FirstOrDefault();

                   // if (_foodItem != null)
                   // {
                        newOrder.OrderItems.Add(new OrderItem { FoodItemId = itemId });
                    //}
                }

                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                result.Message = string.Format("New Order is placed. Order Id : {0}, Order # : {1}", newOrder.Id,newOrder.OrderNumber);
                _logger.LogInformation(string.Format("New order {0} is placed.", newOrder.OrderNumber));
                result.Data = newOrder.OrderNumber; 
            }
            else
            {
                result.IsSuccess = false;
                result.Message = string.Format("Incorrect input params for PlaceOrder method. StudentId = {0} and FoodItems count = {1}",inputOrder.studentId, inputOrder.foodItems.Count);
                _logger.LogCritical(result.Message);
            }

            string json = JsonConvert.SerializeObject(result);
            Request.ContentType = "application/json";
            return Ok(json);
        }

        [HttpGet]
        [Route("byStudentId")]
        public IActionResult GetbyStudentId(int studentId)
        {
            Result result = new Result();
            var orders = _dbContext.Orders.Where<Order>(o => o.StudentId == studentId).ToList<Order>();

            if(orders != null)
            {
                result.IsSuccess = true;
                result.Data = orders;
                result.Message = string.Format("{0} orders found for", orders.Count);

            }

            string json = JsonConvert.SerializeObject(result);
            Request.ContentType = "application/json";
            return Ok(json);

        }
    }
}

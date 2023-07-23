﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using MyCanteenAPI.Models;
using MyCanteenAPI.ViewModels;
using Newtonsoft.Json;

namespace MyCanteenAPI.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        // [HttpPost]
        // [Route("api/[controller]/Auth")]
        
        public IActionResult PostAuthenticate([FromBody]AppCredentials credentials)
        {
            Result resultCall = new Result();
            var context = new MyCanteenDbContext();
            Student? result = context.Students.Where(x => x.StudentNumber.Equals(credentials.UserName) && x.Password.Equals(credentials.Password)).FirstOrDefault();

            if (result != null)
            {
                resultCall.IsSuccess = true;
                resultCall.Data = result;
                
            }
            else
            {
                _logger.LogInformation("Student not found.");
                resultCall.Message = "Student not found";
                return NotFound("Student not found");
            }
           
            string json = JsonConvert.SerializeObject(resultCall);
            Response.ContentType = "application/json";
            _logger.LogInformation("Authentication request received at " + DateTime.Now.ToShortTimeString(), json);
            return Ok(json);
            
        }

        //[HttpGet]
        //public string Get()
        //{
        //    _logger.LogInformation("Received GET request at " + DateTime.Now.ToShortTimeString());
        //    return "Received GET request at " + DateTime.Now.ToShortTimeString();
        //}

    }
}

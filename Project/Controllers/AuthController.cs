using Domains.DTos;
using Domains.Enums;
using Domains.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUser _userService;
        private readonly ICountry _countryService;

        public AuthController(IUser userService, ICountry countryService)
        {
            _userService = userService;
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult Auth()
        {
            var countries = _countryService.GetAllCountries();
            ViewBag.Countries = countries;

            var response = new AddUserResponse();

            return View(response);
        }
        [HttpPost]
        public IActionResult Register(AddUserRequest request)
        {
            var response = new AddUserResponse();

            var countries = _countryService.GetAllCountries();
            ViewBag.Countries = countries;

            try
            {
                if (request == null)
                    return BadRequest("Invalid request data.");

                if (string.IsNullOrWhiteSpace(request.UserName) && string.IsNullOrWhiteSpace(request.EmailAddress) && string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest("Username, Email Address, and Password cannot be empty.");

                if (string.IsNullOrWhiteSpace(request.Country))
                    return BadRequest("Country cannot be empty.");

                var dto = new AddUserRequestDto
                {
                    UserName = request.UserName,
                    EmailAddress = request.EmailAddress,
                    Password = request.Password,
                    Country = request.Country
                };

                var addUserResponse = _userService.CreateUser(dto);

                if(addUserResponse.Status == OpStatus.AlreadyExist)
                {
                    response.Status = OpStatus.Warning;
                    response.Message = "Email Address or Username already exists.";
                    return View("Auth", response);
                }

                response.Status = OpStatus.Success;
                response.Message = "User registered successfully.";
            }
            catch (Exception)
            {
                response.Status = OpStatus.Error;
                response.Message = "An error occurred while registering the user.";
            }
            return View("Auth", response);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new AddUserResponse();
            return View(response);
        }

        [HttpPost]
        public IActionResult Login(string EmailAddress, string Password)
        {
            var response = new AddUserResponse();
            
            try
            {
                if (EmailAddress == null && Password == null)
                    return BadRequest("Invalid request data.");
                if (string.IsNullOrWhiteSpace(EmailAddress) || string.IsNullOrWhiteSpace(Password))
                    return BadRequest("Email Address and Password cannot be empty.");
                var loginResponse = _userService.Login(EmailAddress, Password);
                if (loginResponse.Status == OpStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    response.Status = OpStatus.Error;
                    response.Message = "Invalid email or password.";
                    return View("Login", response);
                }
            }
            catch (Exception)
            {
                response.Status = OpStatus.Error;
                response.Message = "An error occurred while logging in.";
                return View("Login", response);
            }
            return View("Login", response);
        }

    }
}
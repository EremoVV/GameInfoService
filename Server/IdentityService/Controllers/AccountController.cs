using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using IdentityModel.Client;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityService.Models.Authorization;
using IdentityService.Contexts;
using IdentityService.Models;
using IdentityService.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IEventService _events; //Event logging system

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
        }
        [HttpGet("[action]")]
        public RedirectResult Index()
        {
            return Redirect("https://localhost:5000/.well-known/openid-configuration");
        }

        [HttpGet("[action]")]
        public JsonResult GetUsers()
        {
            return new JsonResult(_userManager.Users.ToList());
        }

        [HttpPost("[action]")]
        public async Task<string> RegisterUser(UserRegisterView userRegister)
        {
            if (TryValidateModel(userRegister))
            {
                User newUser = new User
                {
                    UserName = userRegister.UserName,
                    Email = userRegister.Email,
                    Birthday = userRegister.Birthday,
                    Country = userRegister.Country,
                    City = userRegister.City
                };
                var result = await _userManager.CreateAsync(newUser, userRegister.Password);
                await _userManager.AddToRoleAsync(newUser, "user");
                await _userManager.AddClaimsAsync(newUser, new[]
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.Role, "user")
                });
                if (result.Succeeded) return "Success";
            }
            return "Error";
        }
        [HttpPost("[action]")]
        public async Task<JsonResult> SignInUser(ClientLoginCredentials login)
        {

            //ClientLoginCredentials login = JsonSerializer.Deserialize<ClientLoginCredentials>(creds);

            if (TryValidateModel(login))
            {
                User user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {

                        var client = new HttpClient();
                        var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                        {
                            Address = "https://localhost:5000/connect/token",

                            ClientId = login.ClientId,
                            ClientSecret = login.ClientSecret,

                            Scope = login.Scope,

                            UserName = login.UserName,
                            Password = login.Password
                        });
                        await HttpContext.AuthenticateAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
                        return new JsonResult(new {token = response.AccessToken });
                    }
                }
                else
                {
                    ModelState.AddModelError("User", "User not found");
                }
            }

            return new JsonResult(new {data = ModelState});

        }

        [HttpGet("[action]")]
        public async Task<JsonElement> GetUserInfo(string bearerToken)
        {
            var client = new HttpClient();

            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = "https://localhost:5000/connect/userinfo",
                Token = bearerToken
            });

            return response.Json;
        }

        [HttpDelete("[action]")]
        public async void DeleteUser(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> SignOutUser()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            return new JsonResult(new {status = "logged out"});
        }
    }
}

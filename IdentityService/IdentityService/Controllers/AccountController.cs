using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityService.Models.Authorization;
using IdentityService.Contexts;
using IdentityService.Models;
using IdentityService.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events; //Event logging system
        //private AuthorizationContext _authorizationContext; Context for Identity

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEventService events, IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            _interaction = interaction;
        }
        [HttpGet("[action]")]
        public JwtSecurityToken Index()
        {
            return new JwtSecurityToken();
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
                if (result.Succeeded) return "Success";
            }
            return "Error";
        }
        [HttpPost("[action]")]
        public async Task<bool> Signin(UserLoginView userLogin)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            if (TryValidateModel(userLogin))
            {
                User user = await _userManager.FindByNameAsync(userLogin.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        principal = await _signInManager.CreateUserPrincipalAsync(user);
                        await HttpContext.SignInAsync(principal);

                        var authContext = await _interaction.GetAuthorizationContextAsync("/home");
                    }
                }
                else
                {
                    ModelState.AddModelError("User", "User not found");
                }
            }
            return principal.IsAuthenticated();
        }
        [HttpDelete("[action]")]
        public async void DeleteUser(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
        }

        [HttpGet("[action]")]
        public async Task<RedirectResult> ViewUserInfo()
        {
            return Redirect("https://localhost:44366/connect/userinfo");
        }

        [HttpPost("[action]")]
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return "Logged out";
        }

        [HttpGet("[action]")]
        public async Task<string> GetToken()
        {
            return await HttpContext.GetTokenAsync("Bearer");
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<bool> IsAuthenticated()
        {
            var result = await HttpContext.AuthenticateAsync();
            return result.Succeeded;
        }

    }
}

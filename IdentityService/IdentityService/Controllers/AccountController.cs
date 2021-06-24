using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityService.Models.Authorization;
using IdentityService.Contexts;


namespace IdentityService.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private AuthorizationContext _authorizationContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AuthorizationContext authorizationContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authorizationContext = authorizationContext;
        }
        [HttpGet]
        public JwtSecurityToken Index()
        {
            return new JwtSecurityToken();
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            return new JsonResult(_userManager.Users.ToList());
        }

        [HttpPost]
        public async Task<string> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) return "Success";
            return "Error";
        }
        [HttpPost]
        public async Task<string> Signin(User user, string password)
        {
            if (!await _signInManager.CanSignInAsync(user))
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    await _signInManager.SignInAsync(user, false);
                    return "Signed in";
                }
                return "Incorrect password";
            }
            return "User already is signed in";
        }
        [HttpDelete]
        public async void DeleteUser(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
        }
    }
}

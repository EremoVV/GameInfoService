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
using GameInfoService.IdentityService.Models.Authorization;
using GameInfoService.IdentityService.Contexts;
using GameInfoService.IdentityService.Models;
using GameInfoService.IdentityService.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;

namespace GameInfoService.IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        private readonly IEventService _events; //Event logging system

        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
        }
        [HttpGet]
        public RedirectResult Index()
        {
            return Redirect("https://localhost:5000/.well-known/openid-configuration");
        }

        [HttpGet]
        public ActionResult<List<UserEntity>> GetUsers()
        {
            return Ok(_userManager.Users.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<string>> RegisterUser(UserRegisterDto userRegister)
        {
            if (TryValidateModel(userRegister))
            {
                UserEntity newUser = new UserEntity
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
                if (result.Succeeded) return Ok("User registered");
            }

            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<ActionResult<string>> SignInUser(ClientLoginCredentialsDto login)
        {

            //ClientLoginCredentials login = JsonSerializer.Deserialize<ClientLoginCredentials>(creds);

            if (TryValidateModel(login))
            {
                UserEntity user = await _userManager.FindByNameAsync(login.UserName);
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
                        return Ok(new TokenResponseDto{Token = response.AccessToken});
                    }
                }
                else
                {
                    ModelState.AddModelError("User", "User not found");
                }
            }

            return Ok(ModelState);

        }

        [HttpGet]
        public async Task<ActionResult> GetUserInfo(string bearerToken)
        {
            var client = new HttpClient();

            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = "https://localhost:5000/connect/userinfo",
                Token = bearerToken
            });

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            try
            {
                await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            }
            catch
            {
                return NotFound(id);
            }
            return Ok("User deleted");
        }

        [HttpPost]
        public async Task<ActionResult<string>> SignOutUser()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            return Ok("User logged out");
        }
    }
}

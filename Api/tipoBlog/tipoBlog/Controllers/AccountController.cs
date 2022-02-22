
using BLL.DTO;
using BLL.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using tipoBlog.Models;

namespace tipoBlog.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AccountController : Controller
    {
        UserManager userManager;
        private IConfiguration config;
        public AccountController(IConfiguration configuration, UserManager userManager)
        {
            this.userManager = userManager;
            config = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Registrate([FromBody]UserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!(await userManager.ValidateLogin(user.Login)))
                return BadRequest(new { errors = "Login already exists, choose another one, please" });
            user.PasswordHash = await Task.Run(() => CryptoService.getHash(user.PasswordHash));

            await userManager.CreateAsync(user);

            var claims = await GetClaimsIdentity(new LoginModel() { Login = user.Login, Password = user.PasswordHash });
            if(claims == null)
                return BadRequest(new { errors = "Unfortunately, account wasn't created by some reason" });
            return await getToken(claims);
        }
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            model.Password = await Task.Run(() => CryptoService.getHash(model.Password));
            var claimsIdentity = await GetClaimsIdentity(model);
            if (claimsIdentity == null)
                return BadRequest(new { errors = "Login or password is not correct" });
            return await getToken(claimsIdentity);
        }
        private async Task<JsonResult> getToken(ClaimsIdentity claimsIdentity)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var jwt = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claimsIdentity.Claims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                string accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    access_token = accessToken,
                    username = claimsIdentity.Name
                };

                return Json(response);
            });
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(LoginModel model)
        {
            UserDTO user = await userManager.LoginAsync(model.Login, model.Password);
            if (user == null)
                return null;
            var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString()),
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}

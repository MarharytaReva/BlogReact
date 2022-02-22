using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BLL.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace tipoBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserManager userManager;
        ArticleManager articleManager;
        public UserController(UserManager userManager, ArticleManager articleManager)
        {
            this.userManager = userManager;
            this.articleManager = articleManager;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await userManager.GetItemAsync(id);
            if (user is null)
                return NotFound();
            return user;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("articles/{id}/{pageNumber}")]
        public async Task<ActionResult<List<ArticleDTO>>> GetArticles(int id, int pageNumber)
        {
            int offset = 5;
            var res = await articleManager.GetUserArticles(id, offset, pageNumber);
            if (res is null)
                return NotFound();
            return res;
        }
        
        [HttpGet("news/{id}/{pageNumber}")]
        public async Task<ActionResult<List<ArticleDTO>>> GetNews(int id, int pageNumber)
        {
            int offset = 7;
            var res = await articleManager.GetNews(id, offset, pageNumber);
            if (res is null)
                return NotFound();
            return res;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("subs/{id}/{pageNumber}")]
        public async Task<ActionResult<List<UserDTO>>> GetSubs(int id, int pageNumber)
        {
            int offset = 10;
            var res = await userManager.GetSubs(id, offset, pageNumber);
            if (res is null)
                return NotFound();
            return res;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("publishers/{id}/{pageNumber}")]
        public async Task<ActionResult<List<UserDTO>>> GetPublishers(int id, int pageNumber)
        {
            int offset = 10;
            var res = await userManager.GetPublishers(id, offset, pageNumber);
            if (res is null)
                return NotFound();
            return res;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("search/{searchStr}")]
        public async Task<ActionResult<List<UserDTO>>> Search(string searchStr)
        {
            var res = await userManager.Search(searchStr);
            if (res is null)
                return NotFound();
            return res;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            UserDTO user = await userManager.GetItemAsync(id);
            if (user is null)
                return NotFound();
            await userManager.DeleteAsync(user);

            return Ok(user);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<ActionResult<UserDTO>> Put(UserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            UserDTO foundUser = await userManager.GetItemAsync(user.UserId);
            if (foundUser is null)
                return NotFound();

            await userManager.UpdateAsync(user);

            return Ok(user);
        }
    }
}

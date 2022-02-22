using BLL.DTO;
using BLL.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tipoBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        ArticleManager articleManager;
        public ArticleController(ArticleManager articleManager)
        {
            this.articleManager = articleManager;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> Get(int id)
        {
            var article = await articleManager.GetItemAsync(id);
            if (article is null)
                return NotFound();
            return article;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<ArticleDTO>> Post(ArticleDTO article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await articleManager.CreateAsync(article);

            return Ok(article);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticleDTO>> Delete(int id)
        {
            ArticleDTO article = await articleManager.GetItemAsync(id);
            if (article is null)
                return NotFound();
            await articleManager.DeleteAsync(article);

            return Ok(article);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<ActionResult<ArticleDTO>> Put(ArticleDTO article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ArticleDTO foundArticle = await articleManager.GetItemAsync(article.ArticleId);
            if (foundArticle is null)
                return NotFound();

            await articleManager.UpdateAsync(article);

            return Ok(article);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Managers;
using BLL.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace tipoBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        SubscribeManager subscribeManager;
       public SubscribeController(SubscribeManager subscribeManager)
        {
            this.subscribeManager = subscribeManager;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<SubscriptionDTO>> Post(SubscriptionDTO subscription)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await subscribeManager.CreateAsync(subscription);

            return Ok(subscription);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{subId}/{publisherId}")]
        public async Task<ActionResult<SubscriptionDTO>> Get(int subId, int publisherId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var subscription = await subscribeManager.GetItemAsync(subId, publisherId);
            if (subscription is null)
                return NotFound();
            return Ok(subscription);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{subId}/{publisherId}")]
        public async Task<ActionResult<SubscriptionDTO>> Delete(int subId, int publisherId)
        {
            SubscriptionDTO subscription = await subscribeManager.GetItemAsync(subId, publisherId);
            if (subscription is null)
                return NotFound();
            await subscribeManager.DeleteAsync(subscription);

            return Ok(subscription);
        }
    }
}

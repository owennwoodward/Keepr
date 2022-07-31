using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<List<Keep>> Get()
        {
            try
            {
                List<Keep> keeps = _ks.Get();
                return Ok(keeps);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = userInfo.Id;
                Keep newKeep = _ks.Create(keepData);
                keepData.Creator = userInfo;
                keepData.CreatedAt = new DateTime();
                keepData.UpdatedAt = new DateTime();
                return Ok(newKeep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Keep>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep keep = _ks.GetById(id, userInfo?.Id);
                return Ok(keep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.Id = id;
                keepData.CreatorId = userInfo.Id;
                Keep update = _ks.Edit(keepData);
                return Ok(update);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep deletedKeep = _ks.Delete(id, userInfo.Id);
                return Ok(deletedKeep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }



    }
}
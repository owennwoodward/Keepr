using System;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;

        public ProfilesController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> getById(string id)
        {
            try
            {
                Profile profile = _accountService.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }
    }
}
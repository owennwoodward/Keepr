using System;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

    [ApiController]
    [Route("api[controller]")]
    public class ProfilesController
    {
        private readonly AccountService _accountserv;

        public ProfilesController(AccountService accountserv)
        {
            _accountserv = accountserv;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> getById(string id)
        {
            try
            {
                Profile profile = _accountserv.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception)
            {

                return BadRe
            }
        }
    }
}
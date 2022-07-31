using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        private readonly ProfilesService _ps;

        public ProfilesController(AccountService accountService, KeepsService ks, VaultsService vs, ProfilesService ps)
        {
            _accountService = accountService;
            _ks = ks;
            _vs = vs;
            _ps = ps;
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

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _ps.GetKeeps(id);
                return Ok(keeps);
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }

        [HttpGet("{id}/vaults")]

        public async Task<ActionResult<List<Vault>>> GetVaults(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _ps.GetVaults(id, userInfo?.Id);
                return Ok(vaults);
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }
    }
}
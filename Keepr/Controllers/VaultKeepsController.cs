using System;
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
    public class VaultKeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;

        public VaultKeepsController(KeepsService ks, VaultsService vs, VaultKeepsService vks)
        {
            _ks = ks;
            _vs = vs;
            _vks = vks;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                if (userInfo == null)
                {
                    throw new Exception("You are not who you say you are");
                }
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep newVaultKeep = _vks.Create(vaultKeepData, userInfo?.Id);
                return Ok(newVaultKeep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult<string> Delete(int id)
        {
            try
            {
                _vks.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }



    }
}
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
    public class VaultsController : ControllerBase
    {
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;

        public VaultsController(KeepsService ks, VaultsService vs, VaultKeepsService vks)
        {
            _ks = ks;
            _vs = vs;
            _vks = vks;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault newVault = _vs.Create(vaultData);
                vaultData.Creator = userInfo;
                return Ok(newVault);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetById(id, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<KeepVaultViewModel>>> GetKeeps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<KeepVaultViewModel> keeps = _vks.GetKeeps(id, userInfo?.Id);
                return Ok(keeps);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.Id = id;
                vaultData.CreatorId = userInfo.Id;
                Vault update = _vs.Edit(vaultData);
                return Ok(update);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Vault>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault deletedVault = _vs.Delete(id, userInfo.Id);
                return Ok(deletedVault);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }



    }
}
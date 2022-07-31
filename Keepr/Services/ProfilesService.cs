using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _prepo;
        private readonly AccountService _accountService;

        public ProfilesService(ProfilesRepository prepo, AccountService accountService)
        {
            _prepo = prepo;
            _accountService = accountService;
        }

        internal List<Keep> GetKeeps(string id)
        {
            Profile find = _accountService.GetProfile(id);
            return _prepo.GetKeeps(find.Id);

        }

        internal List<Vault> GetVaults(string id, string userId)
        {
            Profile find = _accountService.GetProfile(id);
            if (find.Id != userId)
            {
                return _prepo.GetVaults(find.Id);
            }
            return _prepo.GetPrivateVaults(find.Id);
        }
    }
}
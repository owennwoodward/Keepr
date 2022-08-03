using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _repo;
        private readonly KeepsRepository _krepo;
        private readonly VaultsRepository _vrepo;

        public VaultKeepsService(VaultKeepsRepository repo, KeepsRepository krepo, VaultsRepository vrepo)
        {
            _repo = repo;
            _krepo = krepo;
            _vrepo = vrepo;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
        {
            Vault exists = _vrepo.GetById(vaultKeepData.VaultId);
            if (exists.CreatorId != userId)
            {
                throw new Exception("Not your VaultKeep");
            }
            VaultKeep newVaultKeep = _repo.Create(vaultKeepData);
            Keep keep = _krepo.GetById(newVaultKeep.KeepId);
            keep.Kept++;
            _krepo.Edit(keep);
            return newVaultKeep;
        }

        internal List<KeepVaultViewModel> GetKeeps(int id, string userId)
        {
            Vault found = _vrepo.GetById(id);
            if (found.CreatorId != userId && found.IsPrivate == true)
            {
                throw new Exception("Not your vault");
            }
            return _repo.GetKeeps(id);
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = Get(id);
            if (found.CreatorId != userId)
            {
                throw new Exception("Not your VaultKeep");
            }
            _repo.Delete(id);
            Keep keep = _krepo.GetById(found.KeepId);
            keep.Kept--;
            _krepo.Edit(keep);
        }

        internal VaultKeep Get(int id)
        {
            VaultKeep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("No such Id");
            }

            return found;
        }
    }
}
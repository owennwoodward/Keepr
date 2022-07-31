using System;
using System.Collections.Generic;
using Keepr.Models;

using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _vrepo;

        public VaultsService(VaultsRepository vrepo)
        {
            _vrepo = vrepo;
        }


        internal Vault Create(Vault vaultData)
        {
            return _vrepo.Create(vaultData);
        }

        internal Vault GetById(int vaultId, string userId)
        {
            Vault found = _vrepo.GetById(vaultId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            if (found.IsPrivate && found.CreatorId != userId)
            {
                throw new Exception("Not your Vault");
            }
            return found;
        }

        internal Vault Edit(Vault vaultData)
        {
            Vault original = _vrepo.GetById(vaultData.Id);
            if (vaultData.CreatorId != original.CreatorId)
            {
                throw new Exception("Not your Vault to Edit");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;

            _vrepo.Edit(original);
            return original;

        }

        internal Vault Delete(int id, string userId)
        {
            Vault original = _vrepo.GetById(id);
            if (userId != original.CreatorId)
            {
                throw new Exception("Not your Vault to Delete");
            }

            _vrepo.Delete(id);
            return original;
        }

        internal List<Vault> GetVaults(string id)
        {
            return _vrepo.GetVaults(id);
        }
    }
}
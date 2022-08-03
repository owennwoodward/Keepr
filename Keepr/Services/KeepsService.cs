using System;
using System.Collections.Generic;
using Keepr.Models;

using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _krepo;

        public KeepsService(KeepsRepository krepo)
        {
            _krepo = krepo;
        }

        internal List<Keep> Get()
        {

            return _krepo.Get();

        }

        internal Keep Create(Keep keepData)
        {
            return _krepo.Create(keepData);
        }

        internal Keep GetById(int keepId, string userId)
        {
            Keep found = _krepo.GetById(keepId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            found.Views++;
            _krepo.Edit(found);
            return found;
        }

        internal Keep Edit(Keep keepData)
        {
            Keep original = _krepo.GetById(keepData.Id);
            if (keepData.CreatorId != original.CreatorId)
            {
                throw new Exception("Not your Keep");
            }
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;

            _krepo.Edit(original);
            return original;

        }

        internal Keep Delete(int id, string userId)
        {
            Keep original = _krepo.GetById(id);
            if (userId != original.CreatorId)
            {
                throw new Exception("Not your Keep to Delete");
            }
            _krepo.Delete(id);
            return original;
        }
    }
}
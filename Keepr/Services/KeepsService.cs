using System.Collections.Generic;
using Keepr.Models;

using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal List<Keep> Get()
        {
            return _repo.Get();
        }
    }
}
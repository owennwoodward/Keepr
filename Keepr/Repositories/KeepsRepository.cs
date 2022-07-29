using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> Get()
        {
            string sql = "SELECT * FROM keeps";
            return _db.Query<Keep>(sql).ToList();
        }
    }
}
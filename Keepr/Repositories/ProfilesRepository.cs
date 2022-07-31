using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class ProfilesRepository
    {

        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetKeeps(string id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { id }).ToList();
        }

        internal List<Vault> GetVaults(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.creatorId = @id AND isPrivate = false;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }, new { id }).ToList();
        }

        internal List<Vault> GetPrivateVaults(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }, new { id }).ToList();
        }
    }
}
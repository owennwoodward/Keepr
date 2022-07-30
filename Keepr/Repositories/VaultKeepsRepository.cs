using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {

        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Check(VaultKeep vaultKeepData)
        {
            string sql = @"
            SELECT
            a.*,
            vk.*
            FROM vaultkeeps vk
            JOIN accounts a ON vk.creatorId = p.id
            WHERE vaultId = @ ";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, vaultKeepData);
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            return vaultKeepData;
        }

        internal List<KeepVaultViewModel> GetKeeps(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS VaultKeepId,
            a.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.vaultId = @id;
            ";
            return _db.Query<KeepVaultViewModel, Profile, KeepVaultViewModel>(sql, (VaultKeep, prof) =>
            {
                VaultKeep.Creator = prof;
                return VaultKeep;
            },
            new { id }).ToList();

        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal VaultKeep Get(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
    }
}
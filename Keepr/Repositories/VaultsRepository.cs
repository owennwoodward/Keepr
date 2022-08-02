using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {

        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }


        internal Vault Create(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, img, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal Vault GetById(int vaultId)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @vaultId";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }, new { vaultId }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal void Edit(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate,
            img = @Img,
            creatorId = @CreatorId
            WHERE id = @Id
            ";
            _db.Execute(sql, original);
        }

        internal List<Vault> GetVaults(string id)
        {
            string sql = "SELECT * FROM vaults WHERE vaults.creatorId = @id;";
            return _db.Query<Vault>(sql, new { id }).ToList();
        }
    }
}
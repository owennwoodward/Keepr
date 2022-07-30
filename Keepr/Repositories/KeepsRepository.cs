using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            string sql = @"
            SELECT
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId";
            return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
            {
                keep.Creator = prof;
                return keep;
            }
            ).ToList();
        }

        internal Keep Create(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        internal Keep GetById(int keepId)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @keepId";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { keepId }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal void Edit(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            creatorId = @CreatorId
            WHERE id = @Id
            ";
            _db.Execute(sql, original);
        }
    }
}
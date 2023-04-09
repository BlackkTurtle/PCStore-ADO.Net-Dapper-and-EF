using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class BrandsRepository : GenericRepository<Brands>, IBrandsRepository
    {
        public BrandsRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Brands","BrandID")
        {
        }
    }
}

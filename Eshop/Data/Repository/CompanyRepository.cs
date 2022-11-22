using Dapper;
using Eshop.Data.Models;
using Eshop.ViewModels.Product;
using Microsoft.Data.SqlClient;

namespace Eshop.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _configuration;

        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AddAsync(Company product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Company";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.Query<Company>(sql);
                return result.ToList();
            }
        }

        public Company GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Company product)
        {
            throw new NotImplementedException();
        }
    }
}

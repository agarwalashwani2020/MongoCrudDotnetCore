using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Model;

namespace WebApplication4.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IList<DepartmentModel>> GetAll();
        Task<DepartmentModel> GetById(int id);
        Task InsertOneAsync(DepartmentModel department);
        Task InsertManyAsync(List<DepartmentModel> departments);
        Task UpdateOneAsync(FilterDefinition<DepartmentModel> filter, UpdateDefinition<DepartmentModel> departmentModel);
        Task DeleteOneAsync(FilterDefinition<DepartmentModel> filter);
    }
}

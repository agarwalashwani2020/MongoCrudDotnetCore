using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Middlewares;
using WebApplication4.Model;

namespace WebApplication4.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IMongoCollection<DepartmentModel> _departmentModel;

        public DepartmentRepository(IOptions<AppSettings> options, IDatabaseContext myWorldContext)
        {
            _departmentModel = myWorldContext.Database.GetCollection<DepartmentModel>(options.Value.DepartmentCollection);
        }

        public async Task<IList<DepartmentModel>> GetAll()
        {
            try
            {
                return await _departmentModel.Find(_ => true).ToListAsync();
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException(string.Format("No data found. Error: {0}",e.Message));
            }
        }

        public async Task<DepartmentModel> GetById(int id)
        {
            try
            {
                var data = await _departmentModel.FindAsync(t => t.DepartmentId == id);

                return data.FirstOrDefault();
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException(string.Format("Data not found for Id {0}. Error: {1}",id, e.Message));
            }
        }

        public async Task InsertOneAsync(DepartmentModel department)
        {
            try
            {
                var maxId = _departmentModel.Find(c => true).SortByDescending(d => d.DepartmentId).Limit(1).FirstOrDefault();

                department.DepartmentId = maxId.DepartmentId + 1;

                await _departmentModel.InsertOneAsync(department);
            }
            catch (Exception e)
            {
                throw new Exception("Error while saving");
            }
        }

        public async Task InsertManyAsync(List<DepartmentModel> departments)
        {
            try
            {
                var count = _departmentModel.Find(_ => true).CountDocuments();

                await _departmentModel.InsertManyAsync(departments);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error while saving. Error:{0}",e.Message));
            }
        }

        public async Task UpdateOneAsync(FilterDefinition<DepartmentModel> filter,UpdateDefinition<DepartmentModel> departmentModel)
        {
            try
            {
                await _departmentModel.UpdateOneAsync(filter, departmentModel);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error while updating data. Error:{0}", e.Message));
            }
        }

        public async Task DeleteOneAsync(FilterDefinition<DepartmentModel> filter)
        {
            try
            {
                await _departmentModel.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error while deleting data. Error:{0}", e.Message));
            }
        }
    }
}

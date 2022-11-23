using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Model;
using WebApplication4.Repositories;
using Microsoft.Extensions.Logging;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //private readonly MongoClient mgClient = new MongoClient("mongodb://localhost:27017");

        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentRepository departmentRepository, ILogger<DepartmentController> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            //MongoClient mgClient = new MongoClient("mongodb://localhost:27017");
            //var db = mgClient.GetDatabase("agar2020").GetCollection<DepartmentModel>("myCollOne").Find(c => true).SortByDescending(d => d.DepartmentId).Limit(1).FirstOrDefault();
            //var list = await mgClient.GetDatabase("agar2020").GetCollection<DepartmentModel>("myCollOne").Aggregate().SortByDescending((a) => a.DepartmentId).FirstAsync();

            return Ok(await _departmentRepository.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _departmentRepository.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public string Post(DepartmentModel department)
        {
            _departmentRepository.InsertOneAsync(department);

            return "Added";
        }

        [HttpPost]
        [Route("add-many")]
        public string Post(List<DepartmentModel> departments)
        {
            _departmentRepository.InsertManyAsync(departments);

            return "Added";
        }

        [HttpPut]
        public string Put(DepartmentModel department)
        {
            var filter = Builders<DepartmentModel>.Filter.Eq("DepartmentId", department.DepartmentId);
            var updateDef = Builders<DepartmentModel>.Update.Set(o => o.DepartmentName, department.DepartmentName);
            _departmentRepository.UpdateOneAsync(filter, updateDef);

            return "Updated";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var filter = Builders<DepartmentModel>.Filter.Eq("DepartmentId", id);
            _departmentRepository.DeleteOneAsync(filter);

            return "Deleted";
        }
    }
}
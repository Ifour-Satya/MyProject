using Customer.Microservice.API.Request;
using Customer.Microservice.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public StudentController(IDatabaseService databaseService) 
        {
            _databaseService = databaseService;
        }

        [HttpPost("AddTableProperty")]
        public async Task<IActionResult> AddColumn([FromBody] ColumnAdditionRequest request)
        {
            if (string.IsNullOrEmpty(request.TableName) || request.Columns == null || !request.Columns.Any() ||
                request.Columns.Any(c => string.IsNullOrEmpty(c.ColumnName) || string.IsNullOrEmpty(c.ColumnType)))
            {
                return BadRequest("Table name and at least one valid column (with both name and type) are required.");
            }
           
            try
            {
                foreach (var column in request.Columns)
                {
                    // Dynamically add column to the database table
                    await _databaseService.AddColumnToTableAsync(
                        request.TableName,
                        column.ColumnName,
                        column.ColumnType,
                        column.AllowNull,
                        column.DefaultValue
                    );

                    // Optionally update the class dynamically
                    _databaseService.AddPropertyToClass(
                        request.TableName,
                        column.ColumnName,
                        column.ColumnType
                    );
                }

                return Ok($"Columns added to table {request.TableName}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

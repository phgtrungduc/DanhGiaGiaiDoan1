using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers {
    public class EmployeeController : BaseEntityController<Employee> {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService) {
            _employeeService = employeeService;
        }

        [HttpGet("filter/maxcode")]
        public IActionResult Get() {
            var maxEmployeeCode = _employeeService.GetMaxEmployeeCode();
            return Ok(maxEmployeeCode);
        }
        [HttpGet("filter")]
        public IActionResult GetEmployeeByFilter([FromQuery]string specs, [FromQuery] string DepartmentId, [FromQuery] string PositionId) {
            var maxEmployeeCode = _employeeService.GetEmployeeByFilter(specs, DepartmentId, PositionId);
            return Ok(maxEmployeeCode);
        }
    }
}

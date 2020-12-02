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
        public EmployeeController(IEmployeeService employeeService):base(employeeService) {
            _employeeService = employeeService;
        }
        [HttpGet("search/{param}")]
        public IActionResult Get([FromRoute]string param) {
            var employee = _employeeService.SearchEmployee(param);
            return Ok(employee);
        }
    }
}

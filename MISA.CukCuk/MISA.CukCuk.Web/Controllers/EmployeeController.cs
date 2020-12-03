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
        [HttpGet("search/{param}")]
        public IActionResult Get([FromRoute] string param) {
            var employee = _employeeService.SearchEmployee(param);
            return Ok(employee);
        }
        public override IActionResult Get(string department, string position) {
            if (department == null & position == null) {

                return base.Get(department, position);
            }
            else if (department != null) {
                var employees = _employeeService.GetEmployeeByProperty("Department", department);
                return Ok(employees);
            }
            else {
                var employees = _employeeService.GetEmployeeByProperty("Position", position);
                return Ok(employees);
            }
        }

        [HttpGet("a/b/c/maxcode")]
        public IActionResult Get() {
            var maxEmployeeCode = _employeeService.GetMaxEmployeeCode();
            return Ok(maxEmployeeCode);
        }
    }
}

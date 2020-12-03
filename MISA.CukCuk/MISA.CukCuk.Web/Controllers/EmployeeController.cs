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
        /// <summary>
        /// Lấy mã nhân viên lớn nhất từ client
        /// </summary>
        /// <returns>mã lớn nhất</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        [HttpGet("filter/maxcode")]
        public IActionResult Get() {
            var maxEmployeeCode = _employeeService.GetMaxEmployeeCode();
            return Ok(maxEmployeeCode);
        }
        /// <summary>
        /// FIlter nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="specs">specs có thể là tên,id,code của nhân viên</param>
        /// <param name="DepartmentId">Id phòng ban</param>
        /// <param name="PositionId">id vị trí trong công ty</param>
        /// <returns>Các nhân viên thỏa mãn các tiêu chí trên</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        [HttpGet("filter")]
        public IActionResult GetEmployeeByFilter([FromQuery]string specs, [FromQuery] string DepartmentId, [FromQuery] string PositionId) {
            var maxEmployeeCode = _employeeService.GetEmployeeByFilter(specs, DepartmentId, PositionId);
            return Ok(maxEmployeeCode);
        }
    }
}

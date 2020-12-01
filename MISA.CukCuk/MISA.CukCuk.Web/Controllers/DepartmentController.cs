using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;

namespace MISA.CukCuk.Web.Controllers {
    public class DepartmentController : BaseEntityController<Department> {
        IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) : base(departmentService) {
            _departmentService = departmentService;
        }
    }
}

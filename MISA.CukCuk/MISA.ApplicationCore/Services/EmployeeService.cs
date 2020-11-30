using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services {
    public class EmployeeService : BaseService<Employee>, IEmployeeService {
        IEmployeeRepository _employeeRepository;
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository) {
            _employeeRepository = employeeRepository;
        }
        #endregion
    }
}

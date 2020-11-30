using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure {
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository {

        public EmployeeRepository(IConfiguration configuration) : base(configuration) {
        }

        public Employee GetEmployeeByCode(string employeeCode) {
            var employee = _dbConnection.Query<Employee>($"Select * from {_tableName} where EmployeeCode='{employeeCode}'").FirstOrDefault();
            return employee;
        }
    }
}

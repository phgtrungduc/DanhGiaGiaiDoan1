using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace MISA.Infrastructure {
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository {

        public EmployeeRepository(IConfiguration configuration) : base(configuration) {
        }

        public Employee GetEmployeeByCode(string employeeCode) {
            var employee = _dbConnection.Query<Employee>($"Select * from {_tableName} where EmployeeCode='{employeeCode}'").FirstOrDefault();
            return employee;
        }

        public IEnumerable<Employee> GetEmployeeByProperty(string property, string value) {
            var queryString = $"SELECT * FROM Employee e, Department d, Position p WHERE e.DepartmentId = d.DepartmentId AND e.PositionId = p.PositionId AND e.{property}Id = '{value}'; ";
            var employee = _dbConnection.Query<Employee>(queryString);
            return employee;
        }

        public string GetMaxEmployeeCode() {
            var maxEmployeeCode = _dbConnection.Query<string>("SELECT MAX(EmployeeCode) FROM Employee").FirstOrDefault();
            return maxEmployeeCode.ToString();
        }

        public IEnumerable<Employee> SearchEmployee(string param) {
            var employee = _dbConnection.Query<Employee>("Proc_SearchEmployee", new { param }, commandType: CommandType.StoredProcedure);
            return employee;
        }
    }
}

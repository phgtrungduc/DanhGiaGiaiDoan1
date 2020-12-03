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

        public IEnumerable<Employee> GetEmployeeByFilter(string specs, string DepartmentId, string PositionId) {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode",specs??String.Empty);
            parameters.Add("@FullName",specs??String.Empty);
            parameters.Add("@PhoneNumber",specs??String.Empty);
            parameters.Add("@DepartmentId",DepartmentId);
            parameters.Add("@PositionId", PositionId);
            var employee = _dbConnection.Query<Employee>("Proc_GetEmployeeByFilter",parameters,commandType:CommandType.StoredProcedure);
            return employee;
        }

        public string GetMaxEmployeeCode() {
            var maxEmployeeCode = _dbConnection.Query<string>("SELECT MAX(EmployeeCode) FROM Employee").FirstOrDefault();
            return maxEmployeeCode.ToString();
        }

    }
}

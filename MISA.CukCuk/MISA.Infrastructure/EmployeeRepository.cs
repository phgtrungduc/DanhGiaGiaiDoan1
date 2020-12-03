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
        /// <summary>
        /// Tìm kiếm nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>List các nhân viên có mã thỏa mãn</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        public Employee GetEmployeeByCode(string employeeCode) {
            var employee = _dbConnection.Query<Employee>($"Select * from {_tableName} where EmployeeCode='{employeeCode}'").FirstOrDefault();
            return employee;
        }


        /// <summary>
        /// Tìm kiếm nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="specs">string xét theo id hoặc tên hoặc mã nhân viên</param>
        /// <param name="DepartmentId">id phòng ban</param>
        /// <param name="PositionId">id chức vụ</param>
        /// <returns>các nhân viên thõa mãn các tiêu chí</returns>
        /// CreatedBy:PTDuc(04/12/2020)
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

        /// <summary>
        /// Lấy ra mã khách hàng lớn nhất trên hệ thông
        /// </summary>
        /// <returns>mã lớn nhất</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        public string GetMaxEmployeeCode() {
            var maxEmployeeCode = _dbConnection.Query<string>("SELECT MAX(EmployeeCode) FROM Employee").FirstOrDefault();
            return maxEmployeeCode.ToString();
        }

    }
}

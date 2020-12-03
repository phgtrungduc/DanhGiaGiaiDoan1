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
        /// <summary>
        /// FIlter nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="specs">specs có thể là tên,id,code của nhân viên</param>
        /// <param name="DepartmentId">Id phòng ban</param>
        /// <param name="PositionId">id vị trí trong công ty</param>
        /// <returns>Các nhân viên thỏa mãn các tiêu chí trên</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        public IEnumerable<Employee> GetEmployeeByFilter(string specs, string DepartmentId, string PositionId) {
            return _employeeRepository.GetEmployeeByFilter(specs,DepartmentId,PositionId);
        }
        /// <summary>
        /// Lấy mã nhân viên lớn nhất từ client
        /// </summary>
        /// <returns>mã lớn nhất</returns>
        /// CreatedBy:PTDuc(04/12/2020)
        public string GetMaxEmployeeCode() {
            return _employeeRepository.GetMaxEmployeeCode();
        }

        #endregion
    }
}

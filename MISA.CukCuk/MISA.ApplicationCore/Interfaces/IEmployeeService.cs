using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces {
    public interface IEmployeeService:IBaseService<Employee> {
        string GetMaxEmployeeCode();
        IEnumerable<Employee> GetEmployeeByFilter(string specs, string DepartmentId, string PositionId);
    }
}

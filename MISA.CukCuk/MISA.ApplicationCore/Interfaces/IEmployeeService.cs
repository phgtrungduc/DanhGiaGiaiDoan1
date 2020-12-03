using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces {
    public interface IEmployeeService:IBaseService<Employee> {
        IEnumerable<Employee> SearchEmployee(string param);
        IEnumerable<Employee> GetEmployeeByProperty(string property, string value);
        string GetMaxEmployeeCode();
    }
}

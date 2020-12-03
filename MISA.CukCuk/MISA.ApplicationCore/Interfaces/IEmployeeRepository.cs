﻿using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interfaces {
    public interface IEmployeeRepository : IBaseRepository<Employee> {
        /// <summary>
        /// Lấy thông tin nhân viên theo mã
        /// </summary>
        /// <param name="">mã nhân viên</param>
        /// <returns></returns>
        Employee GetEmployeeByCode(string employeeCode);
        IEnumerable<Employee> SearchEmployee(string param);
        IEnumerable<Employee> GetEmployeeByProperty(string property,string value);
        string GetMaxEmployeeCode();
    }
}

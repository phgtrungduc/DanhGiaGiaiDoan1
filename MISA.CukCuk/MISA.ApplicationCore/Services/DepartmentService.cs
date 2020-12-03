using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services {
    /// <summary>
    /// service lấy tất cả các phòng ban
    /// </summary>
    /// CreatedBy:PTDuc(04/12/2020)
    public class DepartmentService :BaseService<Department>, IDepartmentService {
        IDepartmentRepository _customerRepository;
        public DepartmentService(IDepartmentRepository customerRepository) : base(customerRepository) {
            _customerRepository = customerRepository;
        }
    }
}

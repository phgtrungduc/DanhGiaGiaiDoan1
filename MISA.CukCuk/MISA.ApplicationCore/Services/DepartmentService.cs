using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services {
    public class DepartmentService :BaseService<Department>, IDepartmentService {
        IDepartmentRepository _customerRepository;
        public DepartmentService(IDepartmentRepository customerRepository) : base(customerRepository) {
            _customerRepository = customerRepository;
        }
    }
}

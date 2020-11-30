using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore {
    public class CustomerService : BaseService<Customer>, ICustomerService {
        ICustomerRepository _customerRepository;
        #region Constructor
        //cần phải khởi tạo thằng base bằng cách truyền vào 1 thể hiện của IBaseRepo
        //nhưng bản thân ICustomerRepo đã là thể hiện của nó rồi, đảm bảo không cần dùng đến IBase nên chỉ cần khởi tạo thằng 
        //cu IcustomerRepo
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository) {
            _customerRepository = customerRepository;
        }
        #endregion

        #region Method

        protected override bool ValidateCustom(Customer entity) {
            return base.ValidateCustom(entity);
        }

        public IEnumerable<Customer> GetCustomerPaging(int limit, int offset) {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersByGroupId(Guid groupId) {
            throw new NotImplementedException();
        }
        #endregion
    }
}

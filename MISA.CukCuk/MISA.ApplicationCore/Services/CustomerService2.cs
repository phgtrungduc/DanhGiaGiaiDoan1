//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace MISA.ApplicationCore {
//    public class CustomerService2 {
//        IBaseRepository<Customer> _customerRepository;
//        #region Constructor
//        public CustomerService(IBaseRepository<Customer> customerRepository) : base(customerRepository) {
//            _customerRepository = customerRepository;
//        }
//        #endregion

//        #region Method

//        /// <summary>
//        /// Thêm mới khách hàng
//        /// </summary>
//        /// <returns>object khahs hàng 
//        /// </returns>
//        /// createdBy: PTDuc(25/11/2020)
//        public ServiceResult AddCustomer(Customer customer) {
//            var serviceResult = new ServiceResult();
//            //Validate dữ liệu
//            //Check mã khách hàng không được bỏ trống
//            var customerCode = customer.CustomerCode;
//            if (string.IsNullOrEmpty(customerCode)) {
//                var msg = new {
//                    devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng không được để trống" },
//                    userMsg = "Mã khách hàng không được để trống",
//                    code = MISACode.NotValid
//                };

//                serviceResult.MISACode = MISACode.NotValid;
//                serviceResult.Data = msg;
//                serviceResult.Messenger = "Mã khách hàng không được để trống";
//                return serviceResult;
//            }
//            //check mã khách hàng không được trùng
//            var res = _customerRepository.GetCustomerByCode(customerCode);
//            if (res != null) {
//                var msg = new {
//                    devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng đã tồn tại" },
//                    userMsg = "Mã khách hàng đã tồn tại",
//                    code = MISACode.NotValid
//                };

//                serviceResult.MISACode = MISACode.NotValid;
//                serviceResult.Data = msg;
//                serviceResult.Messenger = "Mã khách hàng không được để trống";
//                return serviceResult;
//            }

//            //Thực hiện thêm dữ liệu
//            var rows = _customerRepository.AddCustomer(customer);
//            serviceResult.Data = rows;
//            serviceResult.Messenger = "Thêm thành công";
//            serviceResult.MISACode = MISACode.IsValid;
//            return serviceResult;
//        }

//        //Xóa khách hàng theo khóa chính
//        public ServiceResult DeleteCustomer(Guid customerID) {
//            throw new NotImplementedException();
//        }


//        public Customer GetCustomerByCode(string customerCode) {
//            var res = _customerRepository.GetCustomerByCode(customerCode);
//            return res;
//        }


//        //Tìm kiếm khách hàng theo 

//        public Customer GetCustomerById(Guid customerId) {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Customer> GetCustomerPaging(int limit, int offset) {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Lấy danh sách khách hàng
//        /// </summary>
//        /// <returns>Danh sách khách hàng</returns>
//        /// createdBy: PTDuc(25/11/2020)
//        public IEnumerable<Customer> GetCustomers() {
//            var customers = _customerRepository.GetCustomers();
//            return customers;
//        }

//        public IEnumerable<Customer> GetCustomersByGroupId(Guid departmentId) {
//            throw new NotImplementedException();
//        }

//        //Sửa thông tin khách hàng
//        public ServiceResult UpdateCustomer(Customer customer) {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}

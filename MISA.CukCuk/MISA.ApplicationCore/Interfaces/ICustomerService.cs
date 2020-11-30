using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.interfaces {
    public interface ICustomerService :IBaseService<Customer>{
        /// <summary>
        /// Phân trang cho dữ liệu customer
        /// </summary>
        /// <param name="limit">số lượng dữ liệu trả về</param>
        /// <param name="offset">vị trí bắt đầu lấy dữ liệu</param>
        /// <returns>dnah sách khách hàng</returns>
        IEnumerable<Customer> GetCustomerPaging(int limit, int offset);

        /// <summary>
        /// Tìm kiếm khách hàng theo nhóm khách hàng
        /// </summary>
        /// <param name="groupId">Id nhóm khách hàng</param>
        /// <returns>Danh sách khách hàng theo nhóm</returns>
        IEnumerable<Customer> GetCustomersByGroupId(Guid groupId);
    }
}

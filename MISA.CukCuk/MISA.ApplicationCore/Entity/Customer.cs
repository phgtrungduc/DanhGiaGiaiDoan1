using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity {
    /// <summary>
    /// Thông tin khách hàng
    /// createdBy:PTDuc(24/11/2020)
    /// </summary>

    public class Customer:BaseEntity {
        #region Declare
        #endregion

        #region Constructor
        public Customer() { }
        #endregion

        #region Properties
        /// <summary>
        /// Id khách hàng
        /// </summary>
        [PrimaryKey]
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã khách hàng KH..
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("mã khách hàng")]
        [MaxLength(20,"Mã khách hàng đã vượt quá 20 kí tự cho phép")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Họ và tên khách hàng
        /// </summary>

        [DisplayName("họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// Ngày tháng năm sinh khách hàng
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính (0 - Nữ; 1 - Nam; 2 - Khác)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Email khách hàng
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary
        [CheckDuplicate]
        [DisplayName("số điện thoại")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }
        /// <summary>
        /// Địa chỉ thường trú
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId { get; set; }
        #endregion

        #region Method
        #endregion
    }
}

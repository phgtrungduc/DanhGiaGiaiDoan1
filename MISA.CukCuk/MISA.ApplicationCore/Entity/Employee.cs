using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MISA.ApplicationCore.Entity {
    public class Employee : BaseEntity {
        #region Declare
        #endregion

        #region Constructor
        public Employee() { }
        #endregion

        #region Properties
        /// <summary>
        /// Id Nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate]
        [DisplayName("mã khách hàng")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên khách hàng
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// Ngày - tháng - năm sinh
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
        /// Số chứng minh nhân dân
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        public DateTime IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp chứng minh nhân
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime JoinDate { get; set; }
        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Mức lương cơ bản
        /// </summary>
        public double Salary { get; set; }
        /// <summary>
        /// Tình trạng làm việc (0:Đang làm việc;1-Đang thử việc;2-Đã nghỉ việc)
        /// </summary>
        public int WorkStatus { get; set; }
        /// <summary>
        /// Id vị trí công việc
        /// </summary>
        public string PositionId { get; set; }
        // <summary>
        /// Tên vị trí công việc
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Id phòng ban làm việc
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban làm việc
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion

        #region Method
        #endregion
    }
}

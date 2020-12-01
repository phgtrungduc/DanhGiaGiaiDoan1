using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity {
    /// <summary>
    /// Thông tin phòng ban 
    /// </summary>
    public class Department: BaseEntity {
        /// <summary>
        /// id của phòng ban
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
    }
}

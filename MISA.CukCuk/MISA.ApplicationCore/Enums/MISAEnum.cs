using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Enums {
    /// <summary>
    /// MISA code để xác định trạng thái của việc validate
    /// </summary>
    public enum MISACode {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid =100,
        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        NotValid = 900,
        /// <summary>
        /// Trạng thái thành công
        /// </summary>
        Success = 200,
        /// <summary>
        /// Exception từ request người dùng gửi lên
        /// </summary>
        Exception = 500

    }
    /// <summary>
    /// Xác định trạng thái của Object
    /// </summary>
    public enum EntityState {
        AddNew = 1,
        Update = 2,
        Delete = 3,
    }
}

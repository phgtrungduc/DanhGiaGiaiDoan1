using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity {
    /// <summary>
    /// Thông tin vị trí/chức vụ 
    /// </summary>
    public class Position:BaseEntity {
        /// <summary>
        /// id chức vụ
        /// </summary>
        public string PositionId { get; set; }
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }
    }
}

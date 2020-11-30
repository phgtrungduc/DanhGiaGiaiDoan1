using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity {
    public class ServiceResult {
        /// <summary>
        /// Dữ liệu trả về (Vd thông tin khách hàng đã thêm)
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Câu thông báo gửi về (Vd: Mã khách hàng bị trùng)
        /// </summary>
        public string Messenger { get; set; }
        /// <summary>
        /// Mã code quy định trog dự án (Vd: 999,900,...)
        /// </summary>
        public MISACode MISACode { get; set; }
    }
}

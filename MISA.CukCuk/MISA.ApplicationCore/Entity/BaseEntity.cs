using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity {
    /// <summary>
    /// attribute cho các trường bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute {
    }
    /// <summary>
    /// attribute cho các trường phải check trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute {
    }
    /// <summary>
    /// attribute cho các trường là khóa chinh
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute {

    }
    /// <summary>
    /// attribute cho các trường là email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : Attribute {

    }
    /// <summary>
    /// attribute cho các trường yêu cầu độ dài tối đa
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute {
        public int Value { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length, string errorMsg) {
            this.Value = length;
            this.ErrorMsg = errorMsg;
        }
    }
    /// <summary>
    /// Phần chung của các đối tượng
    /// </summary>
    public class BaseEntity {
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public EntityState EntityState { get; set; } = EntityState.AddNew;
    }
}

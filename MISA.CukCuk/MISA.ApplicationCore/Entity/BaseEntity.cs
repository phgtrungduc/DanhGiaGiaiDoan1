﻿using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity {

    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute {
        public int Value { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length, string errorMsg) {
            this.Value = length;
            this.ErrorMsg = errorMsg;
        }
    }
    public class BaseEntity {
        //public EntityState EntityState { get; set; } = EntityState.AddNew;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public EntityState EntityState { get; set; } = EntityState.AddNew;
    }
}

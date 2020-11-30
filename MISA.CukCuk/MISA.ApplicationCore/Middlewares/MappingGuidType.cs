﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.ApplicationCore.Middlewares {
    public class MappingGuidType : SqlMapper.TypeHandler<Guid> {
        public override void SetValue(IDbDataParameter parameter, Guid guid) {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value) {
            return new Guid((string)value);
        }
    }
}

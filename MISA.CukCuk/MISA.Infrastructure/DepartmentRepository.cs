using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure {
    public class DepartmentRepository: BaseRepository<Department>, IDepartmentRepository {
        public DepartmentRepository(IConfiguration configuration) : base(configuration) {
        }

    }
}

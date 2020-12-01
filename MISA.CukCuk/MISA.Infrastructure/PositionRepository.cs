using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure {
    public class PositionRepository : BaseRepository<Position>, IPositionRepository {
        public PositionRepository(IConfiguration configuration) : base(configuration) {
        }

    }
}

using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services {
    /// <summary>
    /// Service lấy tất cả cá vị trí
    /// </summary>
    /// CreatedBy:PTDuc(04/12/2020)
    public class PositionService : BaseService<Position>, IPositionService {
        IPositionRepository _positionRepository;
        public PositionService(IPositionRepository positionRepository) : base(positionRepository) {
            _positionRepository = positionRepository;
        }
    }
}

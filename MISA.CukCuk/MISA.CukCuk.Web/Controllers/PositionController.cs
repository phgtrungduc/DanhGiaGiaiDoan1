using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interfaces;

namespace MISA.CukCuk.Web.Controllers {
    public class PositionController : BaseEntityController<Position> {
        IPositionService _positionService;
        public PositionController(IPositionService positionService) : base(positionService) {
            _positionService = positionService;
        }
    }
}

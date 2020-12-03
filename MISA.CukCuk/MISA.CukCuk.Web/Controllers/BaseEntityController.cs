using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MISA.CukCuk.Web.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase {

        IBaseService<TEntity> _baseService;
        public BaseEntityController(IBaseService<TEntity> baseService) {
            _baseService = baseService;
        }

        // GET: api/<CustomersController>
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// createdBy:PTDuc(23/12/2020)
        /// đây là ở thằng cha nhá
        [HttpGet]
        public virtual IActionResult Get(string department, string position) {
                var entities = _baseService.GetEntities();
            return Ok(entities);
        }

        // GET api/<CustomersController>/5
        /// <summary>
        /// Tìm kiếm nhân viên theo ID
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// CreatedBy: PTDuc(4/12/2020)
        [HttpGet("{id}")]
        public IActionResult Get(Guid id) {
            var entity = _baseService.GetEntityById(id);
            return Ok(entity);
        }

        // POST api/<CustomersController>
        /// <summary>
        /// Thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng truyền lên từ client</param>
        /// <returns></returns>
        /// CreatedBy: PTDuc(4/12/2020)
        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity) {

            var serviceResult = _baseService.Add(entity);
            if (serviceResult.MISACode == MISACode.NotValid) {
                return BadRequest(serviceResult);
            }
            else {
                return Ok(serviceResult);
            }

        }
        
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id,[FromBody]TEntity entity) {
            var keyProperty = entity.GetType().GetProperty($"{typeof(TEntity).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid)) {
                keyProperty.SetValue(entity, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int)) {
                keyProperty.SetValue(entity, int.Parse(id));
            }
            else {
                keyProperty.SetValue(entity, id);
            }
            var res = _baseService.Update(entity);
            if (res.MISACode == MISACode.NotValid) {
                return BadRequest(res);
            }
            else {
                return Ok(res);
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) {
            var entity = _baseService.Delete(Guid.Parse(id));
            return Ok(entity);
        }
    }
}

using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace MISA.ApplicationCore {
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity:BaseEntity{
        IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        ResourceManager rm;
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository) {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = Enums.MISACode.Success };
            //rm = new ResourceManager(typeof(ModelEntities.Properties.Resource));
        }

        public virtual ServiceResult Add(TEntity entity) {
            entity.EntityState = Enums.EntityState.AddNew;
            // Thực hiện validate:
            var isValidate = Validate(entity);
            if (isValidate == true) {
                _serviceResult.Messenger = "Thêm thành công";
                _serviceResult.Data = _baseRepository.Add(entity);
                _serviceResult.MISACode = Enums.MISACode.IsValid;
                return _serviceResult;
            }
            else {
                return _serviceResult;
            }
        }

        public int Delete(Guid entity) {
            return _baseRepository.Delete(entity);
        }

        public IEnumerable<TEntity> GetEntities() {
            return _baseRepository.GetEntities();
        }

        public TEntity GetEntityById(Guid entityId) {
            return _baseRepository.GetEntityById(entityId);
        }

        public ServiceResult Update(TEntity entity) {
            entity.EntityState = Enums.EntityState.Update;
            var isValidate = Validate(entity);
            if (isValidate == true) {
                _serviceResult.Messenger = "Update thành công";
                _serviceResult.Data = _baseRepository.Update(entity);
                _serviceResult.MISACode = Enums.MISACode.IsValid;
                return _serviceResult;
            }
            else {
                return _serviceResult;
            }
        }
        #endregion

        /// <summary>
        /// Hàm kiểm tra dữ liệu
        /// </summary>
        /// <param name="entity">object đối tượng cần validate các trường</param>
        /// <returns></returns>
        private bool Validate(TEntity entity) {
            var mesArrayError = new List<string>();
            var isValidate = true;
            // Đọc các Property:
            var properties = entity.GetType().GetProperties();

            //Với mỗi thuộc tính, kiểm tra attribute để validate nó
            foreach (var property in properties) {
                var propertyValue = property.GetValue(entity);

                //sử dụng để kiểm tra attribute DisplayName cuả các thuộc tính Customer hay entity
                var displayName = property.GetCustomAttributes(false)
                .OfType<DisplayNameAttribute>()
                .FirstOrDefault(); ;

                // Kiểm tra xem có attribute cần phải validate không:
                if (property.IsDefined(typeof(Required), false)) {
                    // Check bắt buộc nhập:
                    if (propertyValue == null) {
                        isValidate = false;
                        mesArrayError.Add($"Thông tin {displayName.DisplayName} không được phép để trống.");
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Messenger = "Dữ liệu không hợp lệ";
                    }
                }

                if (property.IsDefined(typeof(CheckDuplicate), false)) {
                    // Check trùng dữ liệu:
                    var entityDuplicate = _baseRepository.GetEntityByProperty(entity, property);
                    //entityDuplicate!=null tức là đã tồn tại 1 thằng có giá trị property trùng trên DB
                    if (entityDuplicate != null) {
                        isValidate = false;
                        mesArrayError.Add($"Thông tin {displayName.DisplayName} đã có trên hệ thống.");
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Messenger = "Dữ liệu không hợp lệ";
                    }
                }
                if (property.IsDefined(typeof(MaxLength), false)) {
                    //Lấy độ dài đã khai báo
                    var attributeMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];
                    var length = (attributeMaxLength as MaxLength).Value;
                    var msg = (attributeMaxLength as MaxLength).ErrorMsg;
                    if (propertyValue.ToString().Trim().Length > length) {
                        isValidate = false;
                        mesArrayError.Add(msg??$"Thông tin {displayName.DisplayName} vượt quá {length} độ dài cho phép");
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Messenger = "Dữ liệu không hợp lệ";
                    }
                }
            }
            _serviceResult.Data = mesArrayError;
            //Với các lớp con kế thừa, nếu muốn validate thêm các thông tin  
            //sẽ override lại phương thức validateCustom và sau đó thực hiện gì kệ nó
            //nhớ phải gọi this.validate không thì nó chỉ gọi của thằng cha mà không gọi đến của thằng con
            if (isValidate == true) isValidate = this.ValidateCustom(entity);
            return isValidate;
        }
        /// <summary>
        /// Hàm validate cho phép override để thực hiện thêm việc custom cho các nghiệp vụ phía sau 
        /// </summary>
        /// <param name="entity">Đối tượng object</param>
        /// <returns>true/false</returns>
        protected virtual bool ValidateCustom(TEntity entity) {
            return true;
        }
    }
}

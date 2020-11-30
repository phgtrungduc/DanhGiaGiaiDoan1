using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure {
    public class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity:BaseEntity{
        protected string _tableName;
        #region DECLARE
        IConfiguration _configuration;
        String _connectionString = null;
        protected IDbConnection _dbConnection = null;
        #endregion

        public BaseRepository(IConfiguration configuration) {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName =  typeof(TEntity).Name; 
        }

        public int Add(TEntity entity) {
            DynamicParameters parameters = MappingDBType(entity);
            //Thực thi 
            var rows = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            //Trả lại số bản
            return rows;
        }

        public int Delete(Guid entityId) {
            //using (_dbConnection.BeginTransaction()) {

            //}
            var rows = _dbConnection.Execute($"Proc_Insert{_tableName}", entityId, commandType: CommandType.StoredProcedure);
            return 0;
        }

        public IEnumerable<TEntity> GetEntities() {
            var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName}", commandType: CommandType.Text);//Chạy câu lệnh đầu query
            //Trả về dữ liệu
            return entities;
        }

        public TEntity GetEntityByCode(Guid entityCode) {
            //Khởi tạo các commandtext, trả về thằng đầu tiên nếu có, nếu không trả về null
            var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName} WHERE CustomerCode='{entityCode}'", commandType: CommandType.Text).FirstOrDefault();//Chạy câu lệnh đầu query
            //Trả về dữ liệu
            return entities;
        }

        public TEntity GetEntityById(Guid entityId) {
            //Khởi tạo các commandtext, trả về thằng đầu tiên nếu có, nếu không trả về null
            var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName} WHERE CustomerCode='{entityId}'", commandType: CommandType.Text).FirstOrDefault();//Chạy câu lệnh đầu query
            //Trả về dữ liệu
            return entities;
        }

        public int Update(TEntity entity) {
            // Khởi tạo kết nối với Db:
            var parameters = MappingDBType(entity);
            // Thực thi commandText:
            var rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả (số bản ghi thêm mới được)
            return rowAffects;
        }

        private DynamicParameters MappingDBType(TEntity entity) {
            var parameters = new DynamicParameters();
            var properties = entity.GetType().GetProperties();
            //Mapping dữ liệu từ C# sang mariadb
            foreach (var property in properties) {
                var propertyName = property.Name;
                var propertyType = property.PropertyType;
                var propertyValue = property.GetValue(entity);
                if (propertyName == $"{_tableName}Id"&& entity.EntityState == EntityState.AddNew) {
                    var newID = Guid.NewGuid();
                    parameters.Add($"{_tableName}Id", newID, DbType.String);
                }
                else {
                    if (propertyType == typeof(Guid) || propertyType == typeof(Guid?)) {
                        parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                    }
                    else {
                        parameters.Add($"@{propertyName}", propertyValue);
                    }
                }
                

            }
            return parameters;

        }
        /// <summary>
        /// Hàm này để dùng chung cho việc tìm kiếm theo 1 tiêu chí nào đó
        /// </summary>
        /// <param name="entity">pbject có thuộc tính cần kiểm tra, gửi thông tin check trong db</param>
        /// <param name="property">thuộc tính cần kiểm tra xem đã tồn tại trong db hay chưa</param>
        /// <returns>trả lại kết quả đầu tiên tìm thấy, nếu không trả lại null</returns>
        public TEntity GetEntityByProperty(TEntity entity, PropertyInfo property) {
            //Tên thuộc tính cần kiểm tra
            var propertyName = property.Name;
            //giá trị thuộc tính cần kiểm tra
            var propertyValue = property.GetValue(entity);
            //lấy giá trị thuộc tính khóa chính (customerID) để phục vụ cho việc query khi update(tìm xem có bản ghi trùng trừ bản ghi đã truyền vào chính là entity)
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = string.Empty;
            if (entity.EntityState == EntityState.AddNew) {
                //Nếu là phương thức thêm thì chỉ cần kiểm tra xem đã tồn tại hay chưa 
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            }

            else if (entity.EntityState == EntityState.Update) {    
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            }
            else
                return null;
            var entityReturn = _dbConnection.Query<TEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
    }
}

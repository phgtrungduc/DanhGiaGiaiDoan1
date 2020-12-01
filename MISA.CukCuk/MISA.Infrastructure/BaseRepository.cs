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
    public class BaseRepository<TEntity>: IBaseRepository<TEntity>,IDisposable where TEntity:BaseEntity{
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
            var rows = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction()) {
                try {
                    DynamicParameters parameters = MappingDBType(entity);
                    //Thực thi 
                    rows = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
                    //Trả lại số bản
                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                }
            }
            return rows;
        }

        public int Delete(Guid entityId) {
            var rows=0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction()) {
                try {
                    rows = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id='{entityId}'", entityId, commandType: CommandType.Text);
                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                }
            }
                
            return rows;
        }

        public IEnumerable<TEntity> GetEntities() {
            var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName}", commandType: CommandType.Text);//Chạy câu lệnh đầu query
            //Trả về dữ liệu
            return entities;
        }


        public TEntity GetEntityById(Guid entityId) {
            //Khởi tạo các commandtext, trả về thằng đầu tiên nếu có, nếu không trả về null
            var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName} WHERE {_tableName}Id='{entityId}'", commandType: CommandType.Text).FirstOrDefault();//Chạy câu lệnh đầu query
            //Trả về dữ liệu
            return entities;
        }

        public int Update(TEntity entity) {
            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction()) {
                try {
                    var parameters = MappingDBType(entity);
                    // Thực thi commandText:
                    rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception) {

                    transaction.Rollback() ;
                }
            }
                
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

        public void Dispose() {
            if (_dbConnection.State == ConnectionState.Open) {
                _dbConnection.Close();
            }

        }
    }
}

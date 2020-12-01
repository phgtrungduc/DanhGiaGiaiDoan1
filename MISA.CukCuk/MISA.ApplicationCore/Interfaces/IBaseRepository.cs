using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.interfaces {
    public interface IBaseRepository<TEntity> {
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(Guid entityId);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(Guid entityId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">object thực thể, ví dụ customer,entity</param>
        /// <param name="property">thuộc tính cần kiểm tra</param>
        /// <returns></returns>
        TEntity GetEntityByProperty(TEntity entity,PropertyInfo property);
    }
}

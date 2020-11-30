using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.interfaces {
    public interface IBaseService<TEntity> {
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(Guid entityId);
        ServiceResult Add(TEntity entity);
        ServiceResult Update(TEntity entity);
        int Delete(Guid entity);
    }
}

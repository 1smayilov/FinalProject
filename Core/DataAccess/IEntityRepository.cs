﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // generic consraint
    // class : referans tip
    // IEntity : IEntity olabilər vəya IEntity implement edən bir nesne
    // new() ; new`lanabilər olmalıdır
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null); // Datanın müəyyən hissəsini gətirmək üçündür | categoryId == 2 olanlar
        Task<T> GetAsync(Expression<Func<T, bool>> filter); // Fərqi odur ki categoryId == 2 olanların içindən ilk tapdığını gətirəcək
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

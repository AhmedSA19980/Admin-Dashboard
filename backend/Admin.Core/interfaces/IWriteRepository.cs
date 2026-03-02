using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces
{
    public interface IWriteRepository<T> where T :class
    {
        Task<T> AddAsync (T entity)  ;

    }
}

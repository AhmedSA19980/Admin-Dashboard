using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces
{
    public interface IUpdateRepository <T> where T : class
    {

        Task<T> UpdateAsync(T entity) ;

    }
}

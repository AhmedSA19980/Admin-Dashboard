using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.user
{
    public interface IUserRepository : IReadRepository ,IWriteRepository , IUpdateRepository 
    {
        Task<T> ChangePassword<T>(T Entity);


    }
}

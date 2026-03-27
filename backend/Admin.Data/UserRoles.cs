using Admin.Core.interfaces;
using Admin.Core.interfaces.UserRoles;
using Admin.Core.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Data
{
    // run migration first
    public class UserRoles : IUserRolesRepository<UserRole>
    {
        private readonly AppDbContext _context;    
        public UserRoles(AppDbContext context) { 
        
            _context = context;
        }

        public async Task<UserRole> AddAsync(UserRole enUserRole)
        {
            await _context.UserRoles.AddAsync(enUserRole);
            await _context.SaveChangesAsync();
            return enUserRole;
          
        }

        public async Task<UserRole> GetByIdAsync(int Id)
        {
            return await _context.UserRoles.FindAsync(Id);
        }

        public async Task<bool> IsRoleExist(int roleId, int userId)
        {
            bool isRoleExist = await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
          
            return isRoleExist;
           
        }
    }
}

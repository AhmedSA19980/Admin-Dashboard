using Admin.Core.interfaces.role;
using Admin.Core.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Data
{
    public class RoleRep :IRoleRepository<Role>
    {
        private readonly AppDbContext _context;

        public RoleRep(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> AllRoles()
        {
           return await _context.Roles.ToListAsync();
        }

        public async Task<Role> FindRoleByName(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public  async Task<Role> GetById(int Id)
        {
            return await  _context.Roles.FindAsync(Id);
        }


    }
}

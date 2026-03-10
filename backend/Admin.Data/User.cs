using Admin.Core.interfaces.user;
using Microsoft.EntityFrameworkCore;
using Admin.Core.models;
using Admin.Core.DTOs.Login;


namespace Admin.Data
{

    public class UserRep : IUserRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRep(AppDbContext context)
        {
            _context = context; 
        }

      
        public async Task<User> AddAsync(User enUser) 
        {
            await _context.Users.AddAsync(enUser);
            await _context.SaveChangesAsync();
            return enUser;
        }

    
        public async Task<bool> ChangePasswordAsync(int userId, string newPass)
        {
            var user = new User { Id = userId, Password = newPass };

            _context.Users.Attach(user);
            _context.Entry(user).Property(u => u.Password).IsModified = true;
            bool res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public Task<User> FindUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User> FindUserByUsernameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<List<User>> ListAllUserAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> UpdateAsync(User enUser)
        {
        
            _context.Users.Update(enUser);
            await _context.SaveChangesAsync();
            return enUser;
        
        }

        public async Task<List<Role>> GetUserRoles(int userId) {

            var userRole = await _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.Role).ToListAsync();

                return userRole;
        
        }
       
      
        public async Task<List<string>> GetUserRoleById(int userId) {

            var user = await _context.UserRoles.Include(u => u.UserId == userId).Select(ur => ur.Role.Name).ToListAsync();

            return user;
        }

        public async Task<User> FindUserByUserNameAndPasswordAsync(LoginDto login) {

              return await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.Login || u.Email == login.Login);

        }
        public async Task<bool> IsEmailTakenAsync(string Email) {

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email ) != null;

        }

        public async Task<bool> IsUserNameTakenAsync(string Email)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == Email) != null;

    }
}
}
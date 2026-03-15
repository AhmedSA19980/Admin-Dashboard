using Admin.Core.interfaces.user;
using Admin.Core.DTOs;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Core.DTOs.users;
using BlobStorage.Core.Global;
using System.ComponentModel;
using Admin.Application.Mappers;

namespace Admin.Application
{
    public class UserService
    {

        private readonly IUserRepository<User> _userRepository;


        public UserService(IUserRepository<User> userRepository) { 
        
        
            _userRepository = userRepository;   
        }



        public async Task<int> AddUserAsync(AddUserDto userDto)
        {

            bool exisitingUser = await _userRepository.IsEmailTakenAsync(userDto.Email);
            if (exisitingUser)
            {

                throw new Exception($"This {userDto.Email} Email  is exist ");
            }

            var newUser = new User { 
                UserName = userDto.UserName,
                Email = userDto.Email,
                First_Name = userDto.First_Name,
                Last_Name = userDto.Last_Name,
                Password = HashPass.hashPassword(userDto.Password)
            };

            await _userRepository.AddAsync(newUser);

            return newUser.Id;


        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto userDto) {

            var exisitingUser = await _userRepository.GetByIdAsync(userDto.Id);
            if (exisitingUser.Id == null)
            {

                throw new Exception($"User with  {userDto.Id} Id is not exist ");
            }

         
            exisitingUser.UserName = userDto.UserName;
            exisitingUser.Email = userDto.Email;
           exisitingUser.First_Name = userDto.First_Name;
            exisitingUser.Last_Name = userDto.Last_Name;

      
            return await _userRepository.UpdateAsync(exisitingUser);

        }

      

        public async Task<UserDto> GetUserById(int id) { 
            
            var user = await _userRepository.GetByIdAsync(id);
           
            return  UserMapper.ToDto(user);
        
        }


        public async Task<UserDto> GetUserByEmail(string email)
        {

            var user = await _userRepository.FindUserByEmailAsync(email);
            return UserMapper.ToDto(user);
        }

        public async Task<UserDto> GetUserByUserName(string username)
        {

            var user = await _userRepository.FindUserByUsernameAsync(username);
            return UserMapper.ToDto(user);

        }
        public async Task<bool> ChangePasswordAsync(changePassDto changePassDto)
        {
            var userEntity = await _userRepository.GetByIdAsync(changePassDto.ID);
            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {changePassDto.ID} not found.");
            }

            string hashPass = HashPass.hashPassword(changePassDto.Password);
            bool newPass =   await _userRepository.ChangePasswordAsync(userEntity.Id , hashPass);
        
            return newPass ;
        }

        public async Task<List<UserDto>> AllUsers() { 
           var  listUsers = await _userRepository.ListAllUserAsync();
           return UsersMapper.ToDtoList(listUsers);
        }

        public async Task< List<string>> GetUserRoles(int userId)
        {
           return  await _userRepository.GetUserRolesById(userId);

        }



    }
}

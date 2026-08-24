using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Repository.Interfaces;
using Messenger.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Messanger.Api.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _UserRepository; 
        private string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
        private string EmailRegex = @"^(?=.{1,254})(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";
        public RegisterService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<Result> RegValidation(RegisterRequest RegRequest)
        {
            var users = await _UserRepository.GetAsync();
            if (string.IsNullOrWhiteSpace(RegRequest.Name) || string.IsNullOrWhiteSpace(RegRequest.OutputName) || string.IsNullOrWhiteSpace(RegRequest.Password) || string.IsNullOrWhiteSpace(RegRequest.Email))
                return new Result(EResultCode.SomeFieldsEmpty, "Некоторые поля имеют нулевые значения!");
            

            if(users.Any(u => u.Name == RegRequest.Name || u.OutputName == RegRequest.OutputName))
            {
                return new Result(EResultCode.Invalid_Field, "ОО люди с таким Именем или Отоб. Именем уже существуют!");
            }

            if (!Regex.IsMatch(RegRequest.Password, PasswordRegex))
                return new Result(EResultCode.Invalid_Field, "Неправильный пароль!");

            if (!Regex.IsMatch(RegRequest.Email, EmailRegex))
                return new Result(EResultCode.Invalid_Field, "Неправильный эмейл!"); 

            var result = await AddUserToDb(RegRequest);

            return new Result(EResultCode.Success, "Успешная регистрация!");
        }

        private async Task<EResultCode> AddUserToDb(RegisterRequest RegRequest)
        {
            try 
            {  
                var passwordHasher = new PasswordHasher<User>();
                User NewUser = new User() { Id = 2, Name = RegRequest.Name, OutputName = RegRequest.OutputName, Email = RegRequest.Email };
                NewUser.Password = passwordHasher.HashPassword(NewUser, RegRequest.Password);
                UserRepository.Users2.Add(NewUser);
                await _UserRepository.AddAsync(NewUser); 
                await _UserRepository.SaveChangesAsync();

                return EResultCode.Success;
            }
            catch 
            {
                return EResultCode.DbError;
            }
        }
    }
} 
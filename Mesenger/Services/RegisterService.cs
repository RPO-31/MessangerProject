using Mesenger.Api.DTO.RequestClasses;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces;
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
        public async Task<Result> RegValidation(RegisterRequestDTO RegRequest)
        {
            var users = await _UserRepository.GetAsync();

            if (string.IsNullOrWhiteSpace(RegRequest.Name) || string.IsNullOrWhiteSpace(RegRequest.OutputName) || string.IsNullOrWhiteSpace(RegRequest.Password) || string.IsNullOrWhiteSpace(RegRequest.Email))
                return new Result(EResultCodes.SomeFieldsEmpty, "Некоторые поля имеют нулевые значения!");


            if (users.Any(u => u.Name == RegRequest.Name || u.OutputName == RegRequest.OutputName))
            {
                return new Result(EResultCodes.Invalid_Field, "ОО люди с таким Именем или Отоб. Именем уже существуют!");
            }

            if (!Regex.IsMatch(RegRequest.Password, PasswordRegex))
                return new Result(EResultCodes.Invalid_Field, "Неправильный пароль!");

            if (!Regex.IsMatch(RegRequest.Email, EmailRegex))
                return new Result(EResultCodes.Invalid_Field, "Неправильный эмейл!");

            var result = await AddUserToDb(RegRequest);

            return new Result(EResultCodes.Success, "Успешная регистрация!");
        }

        private async Task<Result> AddUserToDb(RegisterRequestDTO RegRequest)
        {
            try
            {
                var passwordHasher = new PasswordHasher<User>();
                User NewUser = new User() { Name = RegRequest.Name, OutputName = RegRequest.OutputName, Email = RegRequest.Email, Chats = new List<Chat>(), RegDate = DateTime.Now };

                NewUser.Password = passwordHasher.HashPassword(NewUser, RegRequest.Password);

                await _UserRepository.AddAsync(NewUser);
                await _UserRepository.SaveChangesAsync();

                return new Result(EResultCodes.Success, "Успешно");
            }
            catch
            {
                return new Result(EResultCodes.DbError, "Не удалось загрузить данные");
            }
        }
    }
}
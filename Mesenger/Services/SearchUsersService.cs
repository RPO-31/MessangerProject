using Mesenger.Api.DTO.Transformers;
using Messanger.Api.Enums;
using Messanger.Api.Services.Interfaces;
using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;
using Messenger.Api.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Xml.Linq;

namespace Messanger.Api.Services
{
    public class SearchUsersService : ISearchUsersService
    {

        private readonly IUserRepository _UserRepository;

        public SearchUsersService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<List<UserViewModel>> SearchUsersAsync(string OutputName)
        {
            var users = await _UserRepository.GetAsync();
            var RawResult = users.Where(u => Search(u.OutputName, OutputName)).ToList();

            if (RawResult.Count > 0)
            {
                var usersviewmodel = UserDTO.UsersToViewModel(RawResult);
                return usersviewmodel;
            }
            else
                return new();
        }
        public async Task<UserViewModel> SearchUserByIdAsync(int Id)
        {
            var users = await _UserRepository.GetAsync();

            var RawResult = users.Where(u => u.Id == Id).FirstOrDefault();
            if (RawResult != null)
            {
                var userviewmodel = UserDTO.UserToViewModel(RawResult);
                return userviewmodel;
            }
            else
                return new UserViewModel();
        }
        public bool Search(string Name, string OtherName)
        {
            return string.Equals(Name, OtherName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
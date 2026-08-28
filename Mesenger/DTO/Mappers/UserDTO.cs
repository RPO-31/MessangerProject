using Messanger.Api.ViewModels;
using Messanger.DataAccess.Models;

namespace Mesenger.Api.DTO.Transformers
{
    public static class UserDTO
    {
        public static UserViewModel UserToViewModel(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id, 
                OutputName = user.OutputName
            };
        }
        public static List<UserViewModel> UsersToViewModel(List<User> users)
        {
            var result = new List<UserViewModel>();

            foreach(var user in users)
            {
                result.Add(UserToViewModel(user)); 
            }
            return result;
        }
    }
} 
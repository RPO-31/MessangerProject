using Messanger.Frontend.Enums;
using System.ComponentModel.DataAnnotations;

namespace Messanger.Frontend.Models
{
    public class SearchUserRequest
    {

        public List<int> UsersId { get; set; } = new List<int>();

        public EChatType ChatType { get; set; } = EChatType.None; // None, Private, Group
        public int SelectedId { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        public string SearchName { get; set; }

        public bool ToggleUserId(int userId)
        {
            if (UsersId.Contains(userId))
            {
                UsersId.Remove(userId);
                return false; 
            }
            else
            {
                UsersId.Add(userId);
                return true; 
            }
        }
    }
}
    

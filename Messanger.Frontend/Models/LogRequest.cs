using System.ComponentModel.DataAnnotations;

namespace Messanger.Frontend.Models
{
    public class LogRequest
    {
        [Required(ErrorMessage = " поле обязательно для заполнения")] 
        public string loginOrEmail { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")] 
        public string password { get; set; }
    }
}

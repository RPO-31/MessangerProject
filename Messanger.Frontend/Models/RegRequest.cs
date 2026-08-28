using System.ComponentModel.DataAnnotations;

namespace Messanger.Frontend.Models
{ 
    public class RegRequest
    {

        [Required(ErrorMessage = " поле обязательно для заполнения") ]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "длина от 3 до 50")]
        
        public string login { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "длина от 3 до 50")]
        public string outputName { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.{1,254})(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "Неправильный формат Email!")]
        public string email { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage ="пароль должен содержать: \nзаглавную букву \nспец символ \nминимум 8 букв")]
        public string password { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "пароль должен содержать: \nзаглавную букву \nспец символ \nминимум 8 букв")]
        [Compare("password", ErrorMessage = "Пароли не совпадают!")]
        public string passwordrepeat { get; set; } 
    }
}

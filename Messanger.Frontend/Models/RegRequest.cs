using System.ComponentModel.DataAnnotations;

namespace Messanger.Frontend.Models
{
    public class RegRequest
    {

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "длина от 3 до 12")]
        public string outputName { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "длина от 3 до 12")]
        public string name { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.{1,254})(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "имя содержит только русские и латинские буквы")]
        public string email { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Минимум 8 букв, хотя бы одна заглавная и обычная буква и цифры")]
        public string password { get; set; }

        [Required(ErrorMessage = " поле обязательно для заполнения")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{1,16}$", ErrorMessage = "Минимум 8 букв, хотя бы одна заглавная и обычная буква и цифры")]
        [Compare("password", ErrorMessage = "Пароли не совпадают!")]
        public string passwordrepeat { get; set; }


    }
}

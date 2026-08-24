using System.ComponentModel.DataAnnotations;

namespace Messanger.Frontend.Models
{

    




    public class RegRequest
    {

        [Required(ErrorMessage = " поле обязательно для заполнения") ]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "длина от 3 до 50")]
        
        public string login { get; set; }
        public string email { get; set; }

        public string password { get; set; }

        public string passwordrepeat { get; set; }

        [RegularExpression(@"^[a-zA-Za-яА-ЯёЁ]+$", ErrorMessage ="имя содержит только русские и латинские буквы")]
        public string name { get; set; }


    }
}

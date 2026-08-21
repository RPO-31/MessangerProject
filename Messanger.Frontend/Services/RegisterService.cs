using Messanger.Frontend.Enums;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Messanger.Frontend.Services
{
    public class RegisterService

    {
        public CodeTypes RegValitation(string login, string name, string email, string password, string passwordrepeat)
        {
            string PasswordRegex = @"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@!!%*?&]{8,}$)";

            string EmailRegex = @"^(?=.{1,254})(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordrepeat))
            {
                return CodeTypes.Success;
            }

            //if (!Regex.IsMatch(password, PasswordRegex) || !Regex.IsMatch(passwordrepeat, PasswordRegex) || !password.Equals(passwordrepeat))
                //return NotFound();

           // if (!Regex.IsMatch(email, EmailRegex))
                //return NotFound();

//

            return CodeTypes.Error;
        }

    }
}

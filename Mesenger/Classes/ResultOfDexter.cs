namespace Mesenger.Api.Classes
{
    public class ResultOfDexter
    {
        private bool _isSuccess { get; }
        private string _errorMessage { get; }


        public bool IsSuccess => _isSuccess;
        public string ErrorMessage => _errorMessage;

        public ResultOfDexter(bool IsSuccess, string ErrorMessage)
        {
            _isSuccess = IsSuccess;
            _errorMessage = ErrorMessage;
        }


    }
}

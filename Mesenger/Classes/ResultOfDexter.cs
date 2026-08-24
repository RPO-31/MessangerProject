using Mesenger.Api.Classes;

namespace Mesenger.Api.Classes
{
    public class ResultOfDexter<T>
    {
        private bool _isSuccess { get; }
        private string _errorMessage { get; }
        private T _data { get; } 


        public bool IsSuccess => _isSuccess;
        public T Data => _data;
        public string ErrorMessage => _errorMessage;

        public ResultOfDexter(bool IsSuccess, T data, string ErrorMessage)
        {
            _isSuccess = IsSuccess;
            _errorMessage = ErrorMessage;
        } 
    } 
} 
namespace Messanger.Api.Enums
{
    public struct Result
    {
        public EResultCodes SResultCode;
        public string SMessage;

        public Result(EResultCodes resultCode, string message) { SResultCode = resultCode; SMessage = message; }
    }
    public enum EResultCodes
    {
        Error,
        DbError,
        Success,
        NotExist,
        NotFound,
        SomeFieldsEmpty,
        Invalid_Field,
        HasNotPermission,
        ThisRoomAlreadyExist,
        OutOfLimits,
        Unauthorized
    }
}
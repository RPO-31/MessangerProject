namespace Messanger.Api.Enums
{


    public struct Result
    {
        public EResultCode SResultCode;
        public string SMessage;

        public Result(EResultCode resultCode, string message) { SResultCode = resultCode; SMessage = message; }//{} 
    }
    public enum EResultCode
    {
        Error,
        DbError,
        Success,
        NotExist,
        NotFound,
        SomeFieldsEmpty,
        Invalid_Field,
        HasNotPermission,
        ThisRoomAlreadyExist
    }
}
        
        

     
/*
        Invalid_NameOROutputName,
        Invalid_Password,
        Invalid_Email,*/
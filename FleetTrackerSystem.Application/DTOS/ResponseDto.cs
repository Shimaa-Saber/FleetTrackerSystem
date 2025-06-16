namespace FleetTrackerSystem.Application.DTOS
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public static ResponseDto Success(string message = "")
        {
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = message,
                Data = default
            };
            return response;
        }
        public static ResponseDto Success(object data, string message = "")
        {
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
            return response;
        }
        public static ResponseDto Fail(string message = "")
        {
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = false,
                Message = message,
                Data = default
            };
            return response;
        }
        public static ResponseDto Fail(object errors, string message = "")
        {
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = false,
                Message = message,
                Data = errors
            };
            return response;
        }
    }
}

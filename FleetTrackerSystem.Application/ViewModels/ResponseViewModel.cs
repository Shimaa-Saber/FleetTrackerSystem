using FleetTrackerSystem.Domain.Enums;

namespace FleetTrackerSystem.Application.ViewModels
{
    
    
        public class ResponseViewModel<T>
        {
            public T Data { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public ErrorCode ErrorCode { get; set; }

            public static SuccessResponseViewModel<T> Success(T data, string message = "")
            {
                return new SuccessResponseViewModel<T>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = message,
                    ErrorCode = ErrorCode.None,
                };
            }

            public static ErrorResponseViewModel Error(ErrorCode errorCode, string message = "")
            {
                return new ErrorResponseViewModel
                {
                    Data = default,
                    IsSuccess = false,
                    Message = message,
                    ErrorCode = errorCode,
                };
            }
        }

        public class SuccessResponseViewModel<T> : ResponseViewModel<T>
        {

        }

        public class ErrorResponseViewModel : ResponseViewModel<bool>
        {

        }
    }


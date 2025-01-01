using System.Net;

namespace PaymentProcessingSystem.Core
{
    public class CommonResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ValidationError> Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Details { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }

        public int StatusCode { get; set; }

        public bool Successful { get; set; }

        public string Message { get; set; }

        public string MessageCode { get; set; } = "000000";


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Retry { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object CounterData { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageSize { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageIndex { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageCount
        {
            get
            {
                if (!Total.HasValue || !PageSize.HasValue)
                {
                    return null;
                }

                return (int)Math.Ceiling((double)(int)Total.Value / (double)PageSize.Value);
            }
        }

        public CommonResponse()
        {
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, object data = null, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = true;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, long? total, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool retry, bool successful, long? total, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Retry = retry;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, long? total, object data = null, object dataCounter = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            CounterData = dataCounter;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool retry, bool successful, long? total, object data = null, object dataCounter = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            CounterData = dataCounter;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Retry = retry;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, string details, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Details = details;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, string details, bool retry, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Details = details;
            Retry = retry;
        }

        public CommonResponse(HttpStatusCode httpStatus, string message, bool successful, object data, bool retry, string status = "")
        {
            StatusCode = (int)httpStatus;
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            Retry = retry;
        }

        public CommonResponse(HttpStatusCode httpStatus, ModelStateDictionary modelState, string Message, string status, string requestId)
        {
            ModelStateDictionary modelState2 = modelState;
            base._002Ector();
            StatusCode = (int)httpStatus;
            Successful = false;
            MessageCode = status;
            this.Message = Message;
            Errors = modelState2.Keys.SelectMany((string key) => modelState2[key].Errors.Select((ModelError x) => new ValidationError(key, x.ErrorMessage))).ToList();
            RequestId = requestId;
        }

        public CommonResponse(string message, bool successful, object data = null, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
        }

        public CommonResponse(string message, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = true;
        }

        public CommonResponse(string message, bool successful, long? total, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public CommonResponse(string message, bool successful, long? total, object data = null, object dataCounter = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            CounterData = dataCounter;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public CommonResponse(string message, bool successful, string details, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Details = details;
        }

        public CommonResponse(string message, bool successful, string details, bool retry, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Details = details;
            Retry = retry;
        }

        public CommonResponse(string message, bool successful, object data, bool retry, string status = "")
        {
            Message = message;
            MessageCode = status;
            Successful = successful;
            Data = data;
            Retry = retry;
        }

        public CommonResponse(HttpStatusCode httpStatus, List<ValidationError> ValidationError)
        {
            Successful = false;
            StatusCode = (int)httpStatus;
            Message = "Validation Failed";
            MessageCode = "00020400";
            Errors = ValidationError;
        }

        public CommonResponse(ModelStateDictionary modelState, string Message, string status, string requestId)
        {
            ModelStateDictionary modelState2 = modelState;
            base._002Ector();
            Successful = false;
            MessageCode = status;
            this.Message = Message;
            Errors = modelState2.Keys.SelectMany((string key) => modelState2[key].Errors.Select((ModelError x) => new ValidationError(key, x.ErrorMessage))).ToList();
            RequestId = requestId;
        }

        public static CommonResponse CreateSuccessResponse(HttpStatusCode httpStatus, string message = "", object data = null, string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: true, data, status);
        }

        public static CommonResponse CreateSuccessResponse(HttpStatusCode httpStatus, string message = "", string status = "")
        {
            return new CommonResponse(httpStatus, message, status);
        }

        public static CommonResponse CreatePaginationResponse(HttpStatusCode httpStatus, string message = "", bool retry = false, long? total = null, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            return new CommonResponse(httpStatus, message, retry, successful: true, total, data, pageSize, pageIndex, status);
        }

        public static CommonResponse CreatePaginationResponse(HttpStatusCode httpStatus, string message = "", long? total = null, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: true, total, data, pageSize, pageIndex, status);
        }

        public static CommonResponse CreatePaginationResponseWithCounters(HttpStatusCode httpStatus, string message = "", long? total = null, object data = null, object dataCounter = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: true, total, data, dataCounter, pageSize, pageIndex, status);
        }

        public static CommonResponse CreateSuccessWithDetailsResponse(HttpStatusCode httpStatus, string message = "", string details = "", string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: true, details, status);
        }

        public static CommonResponse CreateErrorWithDetailsResponse(HttpStatusCode httpStatus, string message = "", string details = "", string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: false, details, status);
        }

        public static CommonResponse CreateFailedResponse(HttpStatusCode httpStatus, string message = "", string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: false, null, status);
        }

        public static CommonResponse CreateFailedResponse(HttpStatusCode httpStatus, string message = "", bool retry = false, string status = "")
        {
            return new CommonResponse(httpStatus, message, successful: false, null, retry, status);
        }

        public static CommonResponse CreateErrorResponse(HttpStatusCode httpStatus, ModelStateDictionary modelState, string Message, string status = "", string requestId = "")
        {
            return new CommonResponse(httpStatus, modelState, Message, status, requestId);
        }

        public static CommonResponse CreateResponse(HttpStatusCode httpStatus, string message = "", bool successful = false, object data = null, string status = "", bool retry = false)
        {
            return new CommonResponse(httpStatus, message, successful, data, retry, status);
        }

        public static CommonResponse CreateSuccessResponse(string message = "", object data = null, string status = "")
        {
            return new CommonResponse(message, successful: true, data, status);
        }

        public static CommonResponse CreateSuccessResponse(string message = "", string status = "")
        {
            return new CommonResponse(message, status);
        }

        public static CommonResponse CreatePaginationResponse(string message = "", long? total = null, object data = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            return new CommonResponse(message, successful: true, total, data, pageSize, pageIndex, status);
        }

        public static CommonResponse CreatePaginationResponseWithCounters(string message = "", long? total = null, object data = null, object Counterdata = null, int? pageSize = null, int? pageIndex = null, string status = "")
        {
            return new CommonResponse(message, successful: true, total, data, Counterdata, pageSize, pageIndex, status);
        }

        public static CommonResponse CreateSuccessWithDetailsResponse(string message = "", string details = "", string status = "")
        {
            return new CommonResponse(message, successful: true, details, status);
        }

        public static CommonResponse CreateErrorWithDetailsResponse(string message = "", string details = "", string status = "")
        {
            return new CommonResponse(message, successful: false, details, status);
        }

        public static CommonResponse CreateFailedResponse(string message = "", string status = "")
        {
            return new CommonResponse(message, successful: false, null, status);
        }

        public static CommonResponse CreateFailedResponse(string message = "", bool retry = false, string status = "")
        {
            return new CommonResponse(message, successful: false, null, retry, status);
        }

        public static CommonResponse CreateBadRequestResponse(HttpStatusCode httpStatus, List<ValidationError> ValidationError)
        {
            return new CommonResponse(httpStatus, ValidationError);
        }

        public static CommonResponse CreateErrorResponse(ModelStateDictionary modelState, string Message, string status = "", string requestId = "")
        {
            return new CommonResponse(modelState, Message, status, requestId);
        }

        public static CommonResponse CreateResponse(string message = "", bool successful = false, object data = null, string status = "", bool retry = false)
        {
            return new CommonResponse(message, successful, data, retry, status);
        }
    }
}

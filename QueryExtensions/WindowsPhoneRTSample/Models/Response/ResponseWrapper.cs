using System.Net;

namespace WindowsPhoneRTSample.Models.Response
{
    public class ResponseWrapper
    {
        public ResponseStatus ResponseStatus { get; set; }
        /// <summary>
        /// Check this value only when response status equals HttpError
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        // public HttpStatusCode? StatusCode { get; set; }
    }

    public class ResponseWrapper<T> : ResponseWrapper
    {
        public T Result { get; set; }
    }

    public enum ResponseStatus
    {
        SuccessWithResult,
        NoInternet,
        SuccessWithNoData,//Operation with server went successfully,but server returns empty response
        HttpError,
        ClientSideError,
    }
}

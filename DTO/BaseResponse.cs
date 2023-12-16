namespace DTO
{
    public class BaseResponse<T> where T : class
    {

        public BaseResponse()
        {
            this.status = ResponseTypeContants.FAIL;
            this.apiStatus = ResponseTypeContants.NOT_COMPLETED;
            this.responseMsg = ResponseTypeContants.FAIL;
        }

        public string status { get; set; }
        public string apiStatus { get; set; }
        public string responseMsg { get; set; }
        public T Data { get; set; }

    }
    public class ResponseTypeContants
    {
        public static string SUCCESS = "SUCCESS";

        public static string ERROR = "ERROR";

        public static string FAIL = "FAIL";

        public static string Forbidden = "Forbidden";

        public static string SUCCESS_WITH_ERROR = "SUCCESS_WITH_ERROR";
        public static string NOT_COMPLETED = "Not Completed";

        public ResponseTypeContants()
        {
        }
    }
}

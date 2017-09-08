namespace eSportMK.MVC.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

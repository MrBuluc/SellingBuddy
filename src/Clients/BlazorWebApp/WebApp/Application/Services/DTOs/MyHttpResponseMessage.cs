namespace WebApp.Application.Services.DTOs
{
    public class MyHttpResponseMessage
    {
        public bool IsSuccessStatusCode { get; set; }
        public dynamic? Content { get; set; }
    }
}

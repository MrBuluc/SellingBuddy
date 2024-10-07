using Newtonsoft.Json;

namespace WebApp.Application.Exceptions
{
    public class ExceptionModel : Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public override string? ToString() => JsonConvert.SerializeObject(this);
    }
}

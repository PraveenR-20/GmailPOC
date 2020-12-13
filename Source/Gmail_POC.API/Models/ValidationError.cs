using Newtonsoft.Json;

namespace Gmail_POC.API.Models
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; }

        public string Message { get; }

        public ValidationError(string Key, string message)
        {
            this.Key = Key != string.Empty ? Key : null;
            Message = message;
        }
    }
}

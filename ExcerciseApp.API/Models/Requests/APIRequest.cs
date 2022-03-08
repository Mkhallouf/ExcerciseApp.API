using System.Collections.Generic;

namespace ExcerciseApp.API.Models.Requests
{
    public class APIRequest<TRequest>
    {
        public TRequest Request { get; set;}
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}

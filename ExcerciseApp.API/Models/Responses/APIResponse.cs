using System.Collections.Generic;

namespace ExcerciseApp.API.Models.Responses
{
    public class APIResponse<TResponse>
    {
        public TResponse Result { get; set;}
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}

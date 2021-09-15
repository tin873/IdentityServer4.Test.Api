using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IdentityServer4.Test.Api.Models
{
    public class ApiResult
    {
        public ResultCode ResultCode { get; set; }
        public string DevMessage { get; set; }
        public string ClientMessage { get; set; }
        public IEnumerable<IdentityError> IdentityError { get; set; }
        public object Data { get; set; }
    }
}

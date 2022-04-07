using CommunicationApi.Adapters.Abstract;
using Microsoft.AspNetCore.Http;

namespace CommunicationApiTest.Adapters.Implementation
{
    public class TestSecurity : AbstractSecurity
    {
        public bool ValidateReturn { get; set; }


        protected override object SecurityStrategy()
        {
            return new {IsTrusted = true};
        }

        protected override bool Validate(HttpContext httpContext)
        {
            return ValidateReturn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUserService.Sdk.Events
{
    public interface UserCreated
    {
        Guid CorrelationId { get; }

        Guid Id { get; }
        string GivenName { get; }
        string FamilyName { get; }
    }
}

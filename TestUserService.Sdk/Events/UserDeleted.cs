using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUserService.Sdk.Events
{
    public interface UserDeleted
    {
        Guid CorrelationId { get; }

        Guid Id { get; }
    }
}

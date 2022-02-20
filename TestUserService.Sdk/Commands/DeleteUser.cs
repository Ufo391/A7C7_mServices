using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUserService.Sdk.Commands
{
    public interface DeleteUser
    {
        Guid CommandId { get; }
        Guid CorrelationId { get; }

        Guid Id { get; }
    }
}

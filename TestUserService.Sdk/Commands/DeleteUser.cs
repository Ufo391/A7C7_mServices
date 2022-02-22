using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTwoAPI.Sdk.Commands
{
    public interface DeleteUser
    {
        Guid CommandId { get; }
        Guid CorrelationId { get; }

        Guid Id { get; }
    }
}

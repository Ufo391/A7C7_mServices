using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTwoAPI.Sdk.Events
{
    public interface UserCreationFailed
    {
        Guid CorrelationId { get; }

        string ErrorMessage { get; }
    }
}

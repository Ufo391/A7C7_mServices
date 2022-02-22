using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTwoAPI.Sdk.Events
{
    public interface UserDeletionFailed
    {
        Guid CorrelationId { get; }

        string ErrorMessage { get; }
    }
}

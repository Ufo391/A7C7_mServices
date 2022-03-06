using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPackages.Messages
{
    public interface IEvent
    {
        Guid CorrelationId { get; }
    }
}

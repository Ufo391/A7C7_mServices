using MessageBroker.Enums;

namespace MessageBroker.Services
{
    public interface IRouterService
    {
        public bool IsRouterOnline();
        public RouterTypes RouterType { get; }
    }
}

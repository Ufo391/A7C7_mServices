using Refit;

namespace MessageBroker.Refit
{
    public interface IRouterApi
    {
        [Get("/ping")]
        public Task<bool> IsRouterOnline();
    }
}

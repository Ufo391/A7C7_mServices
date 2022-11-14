using MessageBroker.Enums;
using MessageBroker.Refit;

namespace MessageBroker.Services
{
    public class RouterService : IRouterService
    {
        private IRouterApi routerApi;
        public RouterTypes RouterType { get; private set; }

        public RouterService(IRouterApi routerApi)
        {
            this.routerApi = routerApi;
            if (IsRouterOnline().Wait())
            {
                throw new NotImplementedException();
            }
            else
            {
                RouterType = RouterTypes.Master;
            }
        }

        public async Task<bool> IsRouterOnline()
        {
            return await routerApi.IsRouterOnline();
        }
    }
}

using CommunicationApi.Adapters;
using CommunicationApi.Adapters.Implementation.Aerial;
using CommunicationApi.Adapters.Implementation.Reliability;
using CommunicationApi.Adapters.Implementation.Security;
using TestPackages.Utils.Enums;

namespace CommunicationApi.Services
{
    public class BundleFactoryService : IBundleFactoryService
    {
        private Dictionary<Guid, Bundle> bundles;

        public BundleFactoryService()
        {
            bundles = new Dictionary<Guid, Bundle>();
        }

        /// <inheritdoc/>
        public void ChangeBundleState(Guid bundleId, ExpertAdvisorStateType state)
        {
            bundles[bundleId].State = state;
        }

        /// <inheritdoc/>
        public IEnumerable<Bundle> GetAllInstancedBundles()
        {
            return bundles.Values.ToList();
        }

        /// <inheritdoc/>
        public Bundle? GetBundle(Guid bundleId)
        {
            if(bundles.ContainsKey(bundleId) == false)
            {
                return null;
            }

            return bundles[bundleId];
        }

        /// <inheritdoc/>
        public Guid InstanceMetaTraderBundle(Guid headId, ExpertAdvisorStateType initState )
        {
            if(bundles is null)
            {
                bundles = new Dictionary<Guid, Bundle>();
            }

            var aerial = new Ae_MetaTrader();
            var security = new Se_MetaTrader();
            var reliability = new Re_MetaTrader();
            var bundle = new Bundle(BundleTypes.MetaTrader, headId, initState, aerial, security, reliability);
            bundles[bundle.Id] = bundle;
            return bundle.Id;
        }
    }
}

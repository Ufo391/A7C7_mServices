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
        public Dictionary<Guid, Bundle> Bundles
        {
            get
            {
                if (bundles == null)
                    bundles = new Dictionary<Guid, Bundle>();
                return bundles;
            }
        }

        /// <inheritdoc/>
        public void ChangeBundleState(Guid bundleId, ExpertAdvisorStateType state)
        {
            Bundles[bundleId].State = state;
        }

        /// <inheritdoc/>
        public IEnumerable<Bundle> GetAllRunningBundles()
        {
            foreach (var bundle in Bundles)
                yield return bundle.Value;
        }

        /// <inheritdoc/>
        public Bundle GetBundle(Guid bundleId)
        {
            return bundles[bundleId];
        }

        /// <inheritdoc/>
        public Guid InstanceMetaTraderBundle(Guid headId, ExpertAdvisorStateType initState )
        {
            var aerial = new Ae_MetaTrader();
            var security = new Se_MetaTrader();
            var reliability = new Re_MetaTrader();
            var bundle = new Bundle(BundleTypes.MetaTrader, headId, initState, aerial, security, reliability);
            Bundles[bundle.Id] = bundle;
            return bundle.Id;
        }
    }
}

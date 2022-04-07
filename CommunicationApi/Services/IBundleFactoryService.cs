using CommunicationApi.Adapters;
using TestPackages.Utils.Enums;

namespace CommunicationApi.Services
{
    public interface IBundleFactoryService
    {
        public Dictionary<Guid, Bundle> Bundles { get; }

        /// <summary>
        /// Instantiates a MetaTrader instance.
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        public Guid InstanceMetaTraderBundle(Guid headId, ExpertAdvisorStateType initState);

        /// <summary>
        /// Returns all running instances of bundles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bundle> GetAllRunningBundles();
        
        /// <summary>
        /// Returns all running instances of bundles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllRunningBundlesAsString();

        /// <summary>
        /// Get Bundle by Id.
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        public Bundle GetBundle(Guid bundleId);
    }
}

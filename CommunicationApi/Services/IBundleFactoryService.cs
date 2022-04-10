using CommunicationApi.Adapters;
using TestPackages.Utils.Enums;

namespace CommunicationApi.Services
{
    public interface IBundleFactoryService
    {
        /// <summary>
        /// Instantiates a MetaTrader instance.
        /// <returns></returns>
        public Guid InstanceMetaTraderBundle(Guid headId, ExpertAdvisorStateType initState);

        /// <summary>
        /// Returns all running instances of bundles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bundle> GetAllInstancedBundles();

        /// <summary>
        /// Get Bundle by Id.
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        public Bundle? GetBundle(Guid bundleId);

        /// <summary>
        /// Changes the state of a bundle.
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="state"></param>
        public void ChangeBundleState(Guid bundleId, ExpertAdvisorStateType state);
    }
}

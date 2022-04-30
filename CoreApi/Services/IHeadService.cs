using CoreApi.Model;

namespace CoreApi.Services
{
    public interface IHeadService
    {
        public void InstanceMetaTraderTicker(SessionModel session);
        public void InstanceMetaTraderExpertAdvisor(SessionModel session);
    }
}

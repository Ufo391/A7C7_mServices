using TestPackages.Utils.Enums;

namespace CommunicationApi.Models.Dtos
{
    public class ChangeBundleStateDto
    {
        public Guid BundleId { get; set; }
        public ExpertAdvisorStateType NewState { get; set; }
    }
}

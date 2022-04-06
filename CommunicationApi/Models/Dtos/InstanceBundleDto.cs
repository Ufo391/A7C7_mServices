using TestPackages.Utils.Enums;

namespace CommunicationApi.Models.Dtos
{
    public class InstanceBundleDto
    {
        public Guid HeadId { get; set; }
        public BundleTypes BundleType { get; set; }
    }
}

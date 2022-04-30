using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CoreApi.Model
{
    public class SessionModel
    {
        [Key]
        public Guid Id { get; set; }
        public string TargetId { get; set; }
        public IPAddress TargetIp { get; set; }
        public string Platform { get; set; }
        public string Strategy { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SessionModel model &&
                   Id.Equals(model.Id) &&
                   TargetId == model.TargetId &&
                   EqualityComparer<IPAddress>.Default.Equals(TargetIp, model.TargetIp);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TargetId, TargetIp);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoreApi.Model
{
    public class ValueModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ValueModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}

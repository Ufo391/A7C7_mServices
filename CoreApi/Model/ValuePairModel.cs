using System.ComponentModel.DataAnnotations;

namespace CoreApi.Model
{
    public class ValuePairModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public ValueModel Base { get; set; }
        [Required]
        public ValueModel Cross { get; set; }
    }
}

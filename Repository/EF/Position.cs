using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string ComplexName { get; set; }
        public string Title { get; set; }
        public long Status { get; set; }
    }
}

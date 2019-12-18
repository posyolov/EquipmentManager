using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    [Table("Positions")]
    public class Position
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string Title { get; set; }
        public long Status { get; set; }
    }
}

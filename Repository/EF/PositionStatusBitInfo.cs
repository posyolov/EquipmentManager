using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    [Table("PositionStatusBitsInfo")]
    public class PositionStatusBitInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BitNumber { get; set; }
        public bool Enable { get; set; }
        public string Title { get; set; }
    }
}

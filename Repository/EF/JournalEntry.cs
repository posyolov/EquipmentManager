using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    [Table("Journal")]
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int Position_Id { get; set; }
        public int? JournalEntryCategory_Id { get; set; }
        public int? PositionStatusBitInfo_BitNumber { get; set; }

        [ForeignKey("Position_Id")]
        public virtual Position Position { get; set; }
        [ForeignKey("JournalEntryCategory_Id")]
        public virtual JournalEntryCategory JournalEntryCategory { get; set; }
        [ForeignKey("PositionStatusBitInfo_BitNumber")]
        public virtual PositionStatusBitInfo PositionStatusBitInfo { get; set; }
    }
}

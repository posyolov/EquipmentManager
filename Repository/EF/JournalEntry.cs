using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    [Table("Journal")]
    public class JournalEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Position_Name { get; set; }
        public int? JournalEntryCategory_Id { get; set; }
        public int? PositionStatusBitInfo_BitNumber { get; set; }
        public bool? IsIncoming { get; set; }

        [ForeignKey("Position_Name")]
        public virtual Position Position { get; set; }
        [ForeignKey("JournalEntryCategory_Id")]
        public virtual JournalEntryCategory JournalEntryCategory { get; set; }
        [ForeignKey("PositionStatusBitInfo_BitNumber")]
        public virtual PositionStatusBitInfo PositionStatusBitInfo { get; set; }
    }
}

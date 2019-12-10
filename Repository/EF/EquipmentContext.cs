using System.Data.Entity;

namespace Repository
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext()
            : base("EquipmentContext")
        {
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<JournalEntry> Journal { get; set; }
        public virtual DbSet<JournalEntryCategory> JournalEntryCategories { get; set; }
        public virtual DbSet<PositionStatusBitInfo> PositionStatusBitsInfo { get; set; }
    }
}

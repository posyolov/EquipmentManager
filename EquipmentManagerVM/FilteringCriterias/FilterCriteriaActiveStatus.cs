﻿using Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EquipmentManagerVM
{
    public class FilterCriteriaActiveStatus : FilterCriteria
    {
        private bool _criteria;
        public bool Criteria
        {
            get => _criteria;
            set
            {
                _criteria = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteriaActiveStatus(string title, bool criteria = true)
            : base(title)
        {
            _criteria = criteria;
        }

        public bool StatusBitActive(JournalEntry entry, ObservableCollection<JournalEntry> entries)
        {
            if (!_enabled)
                return true;

            if (entry == null || entry.Position == null)
                return false;

            if (entry.IsIncoming != true)
                return false;

            if ((entry.Position.Status & (1 << entry.PositionStatusBitInfo_BitNumber)) == 0)
                return false;

            if (entries.Any(e =>
             e.Position_Name == entry.Position_Name
             && e.PositionStatusBitInfo_BitNumber == entry.PositionStatusBitInfo_BitNumber
             && e.IsIncoming == entry.IsIncoming
             && e.DateTime > entry.DateTime))
                return false;

            return true;
        }

    }
}

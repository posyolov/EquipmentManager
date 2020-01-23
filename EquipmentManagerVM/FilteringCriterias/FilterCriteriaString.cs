using System;

namespace EquipmentManagerVM
{
    public class FilterCriteriaString : FilterCriteria
    {
        private string _criteria;
        public string Criteria
        {
            get => _criteria;
            set
            {
                if (value != null)
                {
                    _criteria = value;
                    CriteriaChangedInvoke();
                }
            }
        }

        public FilterCriteriaString(string title, string criteria = "")
            : base(title)
        {
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }

        public bool ContainsIn(string sourceString)
        {
            if (String.IsNullOrEmpty(_criteria) || !_enabled)
                return true;

            if (sourceString == null)
                return false;

            return sourceString.IndexOf(_criteria, StringComparison.OrdinalIgnoreCase) >= 0;
        }

    }
}

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
                _criteria = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteriaString(string title, string criteria = "")
            : base(title)
        {
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }

        public bool ContainsIn(string sourceString)
        {
            if(sourceString == null || _criteria == null)
                return false;

            return !_enabled || sourceString.IndexOf(_criteria, StringComparison.OrdinalIgnoreCase) >= 0;
        }

    }
}

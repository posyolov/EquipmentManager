using System;

namespace EquipmentManagerVM
{
    public class FilterCriteria
    {
        protected bool _enabled;

        public event Action CriteriaChanged;

        public string Title { get; set; }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteria(string title)
        {
            Title = title;
        }

        protected void CriteriaChangedInvoke()
        {
            CriteriaChanged?.Invoke();
        }

    }
}

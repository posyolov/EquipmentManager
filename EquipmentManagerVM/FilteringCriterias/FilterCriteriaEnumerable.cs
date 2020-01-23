using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class FilterCriteriaEnumerable : FilterCriteria
    {
        public IEnumerable<object> CriteriaList { get; }

        object _criteria;
        public object Criteria
        {
            get => _criteria;
            set
            {
                _criteria = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteriaEnumerable(string title, IEnumerable<object> criteriaList, object criteria = null)
                    : base(title)
        {
            CriteriaList = criteriaList ?? throw new ArgumentNullException(nameof(criteriaList));
        }

        public bool EqualsTo(object sourceObject)
        {
            if (_criteria == null || !_enabled)
                return true;

            if (sourceObject == null)
                return false;
            
            return sourceObject.Equals(_criteria);
        }
    }
}

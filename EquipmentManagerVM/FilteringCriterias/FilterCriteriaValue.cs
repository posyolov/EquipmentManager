using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class FilterCriteriaValue<T> : FilterCriteria where T : IComparable
    {
        T _criteria;
        public T Criteria
        {
            get => _criteria;
            set
            {
                _criteria = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteriaValue(string title, T criteria)
                    : base(title)
        {
            _criteria = criteria;
        }

        public bool EqualsTo(T sourceObject)
        {
            if (_criteria == null || !_enabled)
                return true;

            if (sourceObject == null)
                return false;

            return sourceObject.Equals(_criteria);
        }

        public bool NotEqualsTo(T sourceObject)
        {
            if (_criteria == null || !_enabled)
                return true;

            if (sourceObject == null)
                return false;

            return !sourceObject.Equals(_criteria);
        }
    }
}
